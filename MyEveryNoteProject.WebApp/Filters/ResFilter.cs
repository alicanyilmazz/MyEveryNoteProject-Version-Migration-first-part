using MyEveryNoteProject.BusinessLayer;
using MyEveryNoteProject.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEveryNoteProject.WebApp.Filters
{
    public class ResFilter : FilterAttribute, IResultFilter
    {
        LogManager log_mngr = new LogManager();
        public void OnResultExecuted(ResultExecutedContext filterContext) //View calistiktan sonra
        {
            string user = "undefined user";
            if (CurrentSession.User.Username!=null)
            {
                user = CurrentSession.User.Username;
            }
            log_mngr.Insert(new Entities.Log()
            {
                Username = user,
                ActionName = filterContext.RouteData.Values["action"].ToString(),
                ControllerName = filterContext.RouteData.Values["controller"].ToString(),
                ProcessDate = DateTime.Now,
                Information = "OnActionExecuted"

            });
        }

        public void OnResultExecuting(ResultExecutingContext filterContext) //View calismadan once
        {
            string user = "undefined user";
            if (CurrentSession.User.Username != null)
            {
                user = CurrentSession.User.Username;
            }
            log_mngr.Insert(new Entities.Log()
            {
                Username = user,
                ActionName = filterContext.RouteData.Values["action"].ToString(),
                ControllerName = filterContext.RouteData.Values["controller"].ToString(),
                ProcessDate = DateTime.Now,
                Information = "OnResultExecuting"

            });
        }
    }
}