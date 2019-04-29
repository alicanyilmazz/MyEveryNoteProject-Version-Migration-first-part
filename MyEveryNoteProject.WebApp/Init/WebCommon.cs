using MyEveryNoteProject.Common;
using MyEveryNoteProject.Entities;
using MyEveryNoteProject.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEveryNoteProject.WebApp.Init
{
    public class WebCommon : ICommon
    { //Buradaki method aslında DataAccessLayer'ın Web tarafında veri çekeceği zaman ulaştığı metotdur.
        public string GetCurrentUserName()
        {
            EveryNoteUser user = CurrentSession.User;

            if (user != null)
            {
                return user.Username;
            }
            else
            {
                return "By System";
            }


        }
    }
}