using MyEveryNoteProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEveryNoteProject.WebApp.Models
{
    public class CurrentSession
    {
        public static EveryNoteUser User
        {
            get
            {
                return Get<EveryNoteUser>("SignIn");
            }

        }

        public static void Set<T>(string key, T obj)
        {
            HttpContext.Current.Session[key] = obj;
        }

        public static T Get<T>(string key)
        {
            if (HttpContext.Current.Session[key] != null)
            {
                return (T)HttpContext.Current.Session[key];
            }
            else
            {
                return default(T);
            }
        }

        public static void Remove(string key)
        {
            if(HttpContext.Current.Session[key] != null)
            {
                HttpContext.Current.Session.Remove(key);
            }
        }

        public static void Clear()
        {
            HttpContext.Current.Session.Clear();
        }

    }
}

/*
 * Class içerisinde Controllerdaki gibi direk Session[""] diyerek Session'a erişilemez.Class içerisinde kullanmak için ,
 * HttpContext.Current.Session["SignIn"]  şeklinde kullanabiliriz buna dikkat edelim!
 * Session bize object tipi döner unutmayalim ve bizde ona göre "as" ve "is" operatörlerini kullanarak işlem yapalim.  
*/
