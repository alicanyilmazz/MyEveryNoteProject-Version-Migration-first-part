﻿using MyEveryNoteProject.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEveryNoteProject.WebApp.Filters
{
    public class Auth : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (CurrentSession.User==null)
            {
                filterContext.Result = new RedirectResult("/Home/SignIn");
            }
        }
    }
}