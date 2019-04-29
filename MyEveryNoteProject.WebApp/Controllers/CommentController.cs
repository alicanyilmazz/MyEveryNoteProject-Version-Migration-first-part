using MyEveryNoteProject.BusinessLayer;
using MyEveryNoteProject.Entities;
using MyEveryNoteProject.WebApp.Filters;
using MyEveryNoteProject.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyEveryNoteProject.WebApp.Controllers
{
    [ExcFilter]
    public class CommentController : Controller
    {
        private ArticleManager article_mngr = new ArticleManager();
        private CommentManager comment_mngr = new CommentManager();

       
        [HttpGet]
        public ActionResult ShowArticleComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Article article = article_mngr.ListQueryable().Include("Comments").FirstOrDefault(x => x.Id == id);

            if (article == null)
            {
                return HttpNotFound();
            }

            return PartialView("_PartialComments", article.Comments);
        }

        [Auth]
        [ActFilter]
        [ResFilter]
        [HttpPost]
        public ActionResult Edit(int? id, string text)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment = comment_mngr.Find(x => x.Id == id);

            if (comment == null)
            {
                return new HttpNotFoundResult();
            }
            else
            {
                comment.Text = text;

                if (comment_mngr.Update(comment) > 0)
                {
                    return Json(new { result = true }, JsonRequestBehavior.AllowGet); //islem basarili ise
                }
                else
                {
                    return Json(new { result = false }, JsonRequestBehavior.AllowGet); //islem basarisiz ise
                }
            }
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

            Comment comment = comment_mngr.Find(x => x.Id == id);

            if (comment == null)
            {
                return new HttpNotFoundResult();
            }
            else
            {

                if (comment_mngr.Delete(comment) > 0)
                {
                    return Json(new { result = true }, JsonRequestBehavior.AllowGet); //islem basarili ise
                }
                else
                {
                    return Json(new { result = false }, JsonRequestBehavior.AllowGet); //islem basarisiz ise
                }
            }
        }

        [Auth]
        [ActFilter]
        [ResFilter]
        [HttpPost]
        public ActionResult Create(Comment comment, int? articleid)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifierUser");

            if (ModelState.IsValid)
            {
                if (articleid == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Article article = article_mngr.Find(x => x.Id == articleid);

                if (article == null)
                {
                    return new HttpNotFoundResult();
                }

                comment.Articles = article; //Bu comment'in Article'ı kim
                comment.Owner = CurrentSession.User;

                if (comment_mngr.Insert(comment) > 0)
                {
                    return Json(new { result = true }, JsonRequestBehavior.AllowGet); //islem basarili ise
                }

            }

            return Json(new { result = false }, JsonRequestBehavior.AllowGet); //islem basarisiz ise

        }
    }
}