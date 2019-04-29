using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEveryNoteProject.WebApp.ViewModels
{
    public class NotificationViewModelBase<T>
    {
        public List<T> Items { get; set; }
        public string Header { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public string RedirectionMessage { get; set; }
        public bool IsRedirecting { get; set; }
        public string RedirectingUrl { get; set; }
        public int RedirectingTimeout { get; set; }

        public NotificationViewModelBase()
        {
            Header = "INFORMATION";
            RedirectionMessage = "You are being redirected to the homepage.";
            title = "Invalid Operation!";
            IsRedirecting = true;
            RedirectingUrl = "/Home/Index";
            RedirectingTimeout = 10000;
            Items = new List<T>();
        }
    }
}