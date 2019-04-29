using MyEveryNoteProject.BusinessLayer;
using MyEveryNoteProject.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEveryNoteProject.WebApp.Filters
{
    public class ExcFilter : FilterAttribute, IExceptionFilter
    {
        LogManager log_mngr = new LogManager();

        public void OnException(ExceptionContext filterContext)
        {
            string username = "undefined user";
            if (CurrentSession.User != null)
            {
                username = CurrentSession.User.Username;
            }
            log_mngr.Insert(new Entities.Log()
            {
                Username = username,
                ActionName = filterContext.RouteData.Values["action"].ToString(),
                ControllerName = filterContext.RouteData.Values["controller"].ToString(),
                ProcessDate = DateTime.Now,
                ExceptionInformation = "Error : " + filterContext.Exception.Message

            });

            filterContext.ExceptionHandled = true;
            filterContext.Controller.TempData["CatchedException"] = filterContext.Exception;
            filterContext.Result = new RedirectResult("/Home/ExceptionHandling");
        }
    }
}