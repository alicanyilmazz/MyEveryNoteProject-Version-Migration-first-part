using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MyEveryNoteProject.WebApp.App_Start
{
    public class BundleConfig
    {
        public static void BundleManager(BundleCollection bundles)
        {
            // CSS adding - StyleBundle
            bundles.Add(new StyleBundle("~/css/Layout").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/Modals/CommentModal.css",
                "~/Content/toastr.min.css",
                "~/Content/GeneralTheme/css/animate.css",
                "~/Content/GeneralTheme/css/owl.carousel.min.css",
                "~/Content/GeneralTheme/css/owl.theme.default.min.css",
                "~/Content/GeneralTheme/css/magnific-popup.css",
                "~/Content/GeneralTheme/css/aos.css",
                "~/Content/GeneralTheme/css/bootstrap-datepicker.css",
                "~/Content/GeneralTheme/css/jquery.timepicker.css",               
                "~/Content/GeneralTheme/css/style.css",
                "~/Content/GeneralTheme/css/Site.css",
                "~/Content/Fontawesome/css/all.min.css",
                "~/Content/Media.css"

                ));

            bundles.Add(new ScriptBundle("~/js/Layout").Include(
                "~/Scripts/jquery-3.3.1.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Content/Modals/ArticleTextModal.js",
                "~/Content/Modals/CommentModal.js",
                "~/Content/AjaxProcess/TakeArticleBody.js",
                "~/Content/Likes/LikeManagement.js",
                "~/Scripts/toastr.min.js",
                "~/Content/Modals/HomepageImageViewModal.js",
                "~/Content/Uploads/ResumeUpload.js",
                "~/Content/Uploads/ContactFormUpload.js",
                "~/Content/Fontawesome/js/all.min.js",
                "~/Content/GeneralTheme/js/jquery-migrate-3.0.1.min.js",
                "~/Content/GeneralTheme/js/popper.min.js",             
                "~/Content/GeneralTheme/js/jquery.easing.1.3.js",
                "~/Content/GeneralTheme/js/jquery.waypoints.min.js",
                "~/Content/GeneralTheme/js/jquery.stellar.min.js",
                "~/Content/GeneralTheme/js/owl.carousel.min.js",
                "~/Content/GeneralTheme/js/jquery.magnific-popup.min.js",
                "~/Content/GeneralTheme/js/aos.js",
                "~/Content/GeneralTheme/js/jquery.animateNumber.min.js",
                "~/Content/GeneralTheme/js/scrollax.min.js",
                "~/Content/GeneralTheme/js/bootstrap-datepicker.js",
                "~/Content/GeneralTheme/js/jquery.timepicker.min.js",
                "~/Content/GeneralTheme/js/main.js",
                "~/Content/GeneralTheme/js/Site.js"

                ));

            BundleTable.EnableOptimizations = true;
        }
    }
}

