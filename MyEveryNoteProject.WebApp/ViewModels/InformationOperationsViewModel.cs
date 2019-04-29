using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEveryNoteProject.WebApp.ViewModels
{
    public class InformationOperationsViewModel:NotificationViewModelBase<string>
    {
        public InformationOperationsViewModel()
        {
            title = "Information!";
        }
    }
}