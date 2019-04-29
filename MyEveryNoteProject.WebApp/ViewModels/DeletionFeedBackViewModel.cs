//using MyEveryNoteProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEveryNoteProject.WebApp.ViewModels
{
    public class DeletionFeedBackViewModel
    {
        //public EveryNoteUser deletion_user_info { get; set; }
        public string username { get; set; }
        public string ProfileImageFileName { get; set; }
        public string message_validation { get; set; }
        public string username_validation { get; set; }
        public string password_validation { get; set; }
        public int SelectedReasonId { get; set; }
        public SelectList SL_DeletionReasons { get; set; }
    }
}