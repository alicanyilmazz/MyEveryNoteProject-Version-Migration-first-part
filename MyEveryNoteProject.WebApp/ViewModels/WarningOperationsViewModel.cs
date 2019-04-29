using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEveryNoteProject.WebApp.ViewModels
{
    public class WarningOperationsViewModel:NotificationViewModelBase<string>
    {
        public WarningOperationsViewModel()
        {
            title = "Warning!";
        }
    }
}