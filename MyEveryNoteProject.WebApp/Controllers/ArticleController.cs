using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyEveryNoteProject.BusinessLayer;
using MyEveryNoteProject.Entities;
using MyEveryNoteProject.WebApp.Filters;
using MyEveryNoteProject.WebApp.Models;

namespace MyEveryNoteProject.WebApp.Controllers
{
    [ExcFilter]
    public class ArticleController : Controller
    {
        private ArticleManager article_mngr = new ArticleManager();
        private CategoryManager category_mngr = new CategoryManager();
        private EverynoteUserManager everynoteuser_mngr = new EverynoteUserManager();
        private LikedManager liked_mngr = new LikedManager();

        [Auth]
        [ActFilter]
        [ResFilter]
        [HttpGet]
        public ActionResult Index()
        {
            var articles = article_mngr.ListQueryable().Include("Categories").Include("Owner").Where(x => x.Owner.Id == CurrentSession.User.Id).OrderByDescending(x => x.ModifiedOn);//Tablo adlarını Include("Categories") yazmadıgımıza dikkat et!
            return View(articles.ToList());
        }

        [Auth]
        [ActFilter]
        [ResFilter]
        [HttpGet]
        public ActionResult MyLikedNotes()
        {
            var articles = liked_mngr.ListQueryable().Include("LikedUser").Include("Articles").Where(x => x.LikedUser.Id == CurrentSession.User.Id).Select(x => x.Articles).Include("Categories").Include("Owner").OrderByDescending(x => x.ModifiedOn);

            return View("Index", articles.ToList());
        }

        [Auth]
        [ActFilter]
        [ResFilter]
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Article article = article_mngr.Find(x => x.Id == id);

            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }


        [Auth]
        [ActFilter]
        [ResFilter]
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title");
            return View();
        }

        [Auth]
        [ActFilter]
        [ResFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article, HttpPostedFileBase ArticleImage)
        {

            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifierUser");

            if (ModelState.IsValid)
            {

                UploadManager.ArticleImageInsert(article, ArticleImage);

                article.Owner = CurrentSession.User;
                article_mngr.Insert(article);

                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title", article.CategoryId);
            return View(article);
        }

        [Auth]
        [ActFilter]
        [ResFilter]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = article_mngr.Find(x => x.Id == id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title", article.CategoryId);
            return View(article);
        }

        [Auth]
        [ActFilter]
        [ResFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Article article, HttpPostedFileBase ArticleImage)
        {

            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifierUser");

            if (ModelState.IsValid)
            {
                bool result = UploadManager.ArticleImageUpdate(article, ArticleImage);

                Article db_article = article_mngr.Find(x => x.Id == article.Id);
                db_article.IsDraft = article.IsDraft;
                db_article.CategoryId = article.CategoryId;
                db_article.Text = article.Text;
                db_article.Title = article.Title;
                if (result)
                {
                    db_article.ArticleImageFileName = article.ArticleImageFileName;
                }

                article_mngr.Update(db_article);

                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title", article.CategoryId);
            return View(article);
        }

        [Auth]
        [ActFilter]
        [ResFilter]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = article_mngr.Find(x => x.Id == id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        [Auth]
        [ActFilter]
        [ResFilter]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = article_mngr.Find(x => x.Id == id);
            article_mngr.Delete(article);

            return RedirectToAction("Index");
        }

        //[Auth] JS tarafında zaten kontrol ediliyor

        [HttpPost]
        public ActionResult GetLiked(int[] ids)
        {
            if (CurrentSession.User != null)
            {

                List<int> LikedNoteIds = liked_mngr.List(x => x.LikedUser.Id == CurrentSession.User.Id && ids.Contains(x.Articles.Id)).Select(x => x.Articles.Id).ToList();

                return Json(new { result = LikedNoteIds });

            }
            List<int> empty = null;
            return Json(new { result = empty });
        }

        //[Auth] JS tarafında zaten kontrol ediliyor

        [HttpPost]
        public ActionResult SetLikeState(int articleid, bool liked)
        {

            if (CurrentSession.User!=null)
            {


                int res = 0;
                Liked like = liked_mngr.Find(x => x.Articles.Id == articleid && x.LikedUser.Id == CurrentSession.User.Id); //Böyle bir Like var mı?Eğer kullanıcı Likelamış ise bize kayıt gelecek zaten!
                Article article = article_mngr.Find(x => x.Id == articleid); //Peki böyle bir article var mı? database de Admin silmis olabilir vs.

                if (like != null && liked == false) // like!=null demek bu article zaten likelanmış o zaman bize gelen liked==false olması lazım ki daha onceden likelanmış oldugunu anlayalım.
                {
                    res = liked_mngr.Delete(like);
                }
                else if (like == null && liked == true)
                {
                    res = liked_mngr.Insert(new Liked()
                    {

                        LikedUser = CurrentSession.User,
                        Articles = article

                    });
                }

                if (res > 0)
                {
                    if (liked)
                    {
                        article.LikeCount++;
                    }
                    else
                    {
                        article.LikeCount--;
                    }

                    res = article_mngr.Update(article);

                    return Json(new { hasError = false, errorMessage = string.Empty, result = article.LikeCount });
                }


                return Json(new { hasError = true, errorMessage = "liking process could not be performed.", result = article.LikeCount });


            }

            return Json(new { hasError = true, errorMessage = "Please register or login!", result =-1 });

        }


        public ActionResult GetArticleText(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Article article = article_mngr.Find(x => x.Id == id);

            if (article == null)
            {
                return HttpNotFound();
            }


            return PartialView("_PartialArticleText", article);
        }
    }
}
