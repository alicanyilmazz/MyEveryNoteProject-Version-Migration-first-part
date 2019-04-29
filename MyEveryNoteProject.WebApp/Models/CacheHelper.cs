using MyEveryNoteProject.BusinessLayer;
using MyEveryNoteProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace MyEveryNoteProject.WebApp.Models
{
    public class CacheHelper
    {
        public static List<Category> GetCategoriesFromCache()
        {
            var result = WebCache.Get("category-cache");

            if (result == null)
            {
                CategoryManager category_mngr = new CategoryManager();
                result = category_mngr.List();
                WebCache.Set("category-cache", result, 20, true);


            }
            return result;
        }

        public static void ClearCategoriesCache()
        {
            WebCache.Remove("category-cache");
        }

        public static void ClearCache(string key)
        {
            WebCache.Remove(key);
        }
    }
}