using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    [Auth]
    [AuthAdmin]
    [ActFilter]
    [ResFilter]
    [ExcFilter]
    public class CategoryController : Controller
    {
        ArticleManager article_mngr = new ArticleManager();
        CategoryManager category_mngr = new CategoryManager();
        EverynoteUserManager everynoteuser_mngr = new EverynoteUserManager();

        [HttpGet]
        public ActionResult Index()
        {
            return View(category_mngr.List());  //  CacheHelper.GetCategoriesFromCache() ile Cache'den de getirilebilirdi!
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = category_mngr.Find(x => x.Id == id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifierUser");

            if (ModelState.IsValid)
            {
                category_mngr.Insert(category);
                CacheHelper.ClearCategoriesCache();

                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = category_mngr.Find(x => x.Id == id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifierUser");

            if (ModelState.IsValid)
            {
                Category cat = category_mngr.Find(x => x.Id == category.Id);
                cat.Title = category.Title;
                cat.Description = category.Description;

                category_mngr.Update(cat);
                CacheHelper.ClearCategoriesCache();

                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = category_mngr.Find(x => x.Id == id.Value);

            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = category_mngr.Find(x => x.Id == id);

            category_mngr.Delete(category);
            CacheHelper.ClearCategoriesCache();

            return RedirectToAction("Index");
        }

    }
}
