using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEveryNoteProject.WebApp.ViewModels
{
    public class SuccessfulOperationsViewModel:NotificationViewModelBase<string>
    {
        public SuccessfulOperationsViewModel()
        {
            title = "Your Operation is Success."; 
        }
    }
}