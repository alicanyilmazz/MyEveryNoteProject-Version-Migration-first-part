using MyEveryNoteProject.BusinessLayer;
using MyEveryNoteProject.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEveryNoteProject.WebApp.Filters
{
    public class ActFilter : FilterAttribute, IActionFilter
    {
        LogManager log_mngr = new LogManager();
        public void OnActionExecuted(ActionExecutedContext filterContext) //Action Calistiktan Sonra
        {
            string user = "undefined user";
            if (CurrentSession.User.Username != null)
            {
                user = CurrentSession.User.Username;
            }
            log_mngr.Insert(new Entities.Log()
            {
                Username = user,
                ActionName = filterContext.ActionDescriptor.ActionName,
                ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                ProcessDate = DateTime.Now,
                Information = "OnActionExecuted"

            });
        }

        public void OnActionExecuting(ActionExecutingContext filterContext) //Action Calismadan Once
        {
            string user = "undefined user";
            if (CurrentSession.User.Username != null)
            {
                user = CurrentSession.User.Username;
            }
            log_mngr.Insert(new Entities.Log()
            {
                Username = user,
                ActionName = filterContext.ActionDescriptor.ActionName,
                ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                ProcessDate = DateTime.Now,
                Information = "OnActionExecuting"
                

            });
        }
    }
}