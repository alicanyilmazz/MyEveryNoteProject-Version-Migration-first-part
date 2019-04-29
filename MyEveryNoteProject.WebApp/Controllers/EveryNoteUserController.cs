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
using MyEveryNoteProject.BusinessLayer.Results;
using MyEveryNoteProject.Entities;
using MyEveryNoteProject.WebApp.Filters;

namespace MyEveryNoteProject.WebApp.Controllers
{
    [Auth]
    [AuthAdmin]
    [ActFilter]
    [ResFilter]
    [ExcFilter]
    public class EveryNoteUserController : Controller
    {
        ArticleManager article_mngr = new ArticleManager();
        CategoryManager category_mngr = new CategoryManager();
        EverynoteUserManager everynoteuser_mngr = new EverynoteUserManager();

        [HttpGet]
        public ActionResult Index()
        {
            return View(everynoteuser_mngr.List());
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EveryNoteUser everyNoteUser = everynoteuser_mngr.Find(x => x.Id == id.Value);
            if (everyNoteUser == null)
            {
                return HttpNotFound();
            }
            return View(everyNoteUser);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EveryNoteUser everyNoteUser)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifierUser");

            if (ModelState.IsValid)
            {
                BusinessLayerResult<EveryNoteUser> res = everynoteuser_mngr.Insert(everyNoteUser);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message)); //ValidationSummary de error ların çıkmasını sağladık.
                    return View(everyNoteUser);
                }

                string foldername = "user_" + res.Result.Id.ToString();
                string folderpath = Server.MapPath(string.Format("~/images/userimg/{0}", foldername));
                if (!Directory.Exists(folderpath))
                {
                    Directory.CreateDirectory(folderpath);
                }

                return RedirectToAction("Index");
            }

            return View(everyNoteUser);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EveryNoteUser everyNoteUser = everynoteuser_mngr.Find(x => x.Id == id.Value);

            if (everyNoteUser == null)
            {
                return HttpNotFound();
            }
            return View(everyNoteUser);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EveryNoteUser everyNoteUser)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifierUser");

            if (ModelState.IsValid)
            {
                BusinessLayerResult<EveryNoteUser> res = everynoteuser_mngr.Update(everyNoteUser);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message)); //ValidationSummary de error ların çıkmasını sağladık.
                    return View(everyNoteUser);
                }

                return RedirectToAction("Index");
            }
            return View(everyNoteUser);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EveryNoteUser everyNoteUser = everynoteuser_mngr.Find(x => x.Id == id.Value);

            if (everyNoteUser == null)
            {
                return HttpNotFound();
            }
            return View(everyNoteUser);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EveryNoteUser everyNoteUser = everynoteuser_mngr.Find(x => x.Id == id);
            everynoteuser_mngr.Delete(everyNoteUser);

            return RedirectToAction("Index");
        }
    }
}
