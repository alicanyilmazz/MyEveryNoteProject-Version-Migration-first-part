using MyEveryNoteProject.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MyEveryNoteProject.WebApp.Models
{
    public static class UploadManager
    {
        public static void ArticleImageInsert(Article article, HttpPostedFileBase ProfileImage)
        {
            string foldername = "article_" + CurrentSession.User.Id.ToString();
            string folderpath = HttpContext.Current.Server.MapPath(string.Format("~/images/articleimg/{0}", foldername));
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }

            if (ProfileImage != null && (ProfileImage.ContentType == "image/jpeg" || ProfileImage.ContentType == "image/jpg" || ProfileImage.ContentType == "image/png"))
            {
                string filename = $"article_{CurrentSession.User.Id}/article_{Guid.NewGuid()}.{ProfileImage.ContentType.Split('/')[1]}";

                ProfileImage.SaveAs(HttpContext.Current.Server.MapPath($"~/images/articleimg/{filename}"));
                article.ArticleImageFileName = filename;

            }
        }

        public static bool ArticleImageUpdate(Article article, HttpPostedFileBase ProfileImage)
        {
            string foldername = "article_" + CurrentSession.User.Id.ToString();
            string folderpath = HttpContext.Current.Server.MapPath(string.Format("~/images/articleimg/{0}", foldername));
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }

            if (ProfileImage != null && (ProfileImage.ContentType == "image/jpeg" || ProfileImage.ContentType == "image/jpg" || ProfileImage.ContentType == "image/png"))
            {
                string filename = $"article_{CurrentSession.User.Id}/article_{Guid.NewGuid()}.{ProfileImage.ContentType.Split('/')[1]}";

                ProfileImage.SaveAs(HttpContext.Current.Server.MapPath($"~/images/articleimg/{filename}"));
                article.ArticleImageFileName = filename;

                return true;
            }

            return false;
        }

        public static void UserProfileImageFolderCreate(int UserId)
        {
            string foldername = "user_" + UserId.ToString();
            string folderpath = HttpContext.Current.Server.MapPath(string.Format("~/images/userimg/{0}", foldername));
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }
        }

        public static void UserUpdateImageInsert(EveryNoteUser model, HttpPostedFileBase ProfileImage)
        {
            if (ProfileImage != null && (ProfileImage.ContentType == "image/jpeg" || ProfileImage.ContentType == "image/jpg" || ProfileImage.ContentType == "image/png"))
            {
                string filename = $"user_{model.Id}/user_{Guid.NewGuid()}.{ProfileImage.ContentType.Split('/')[1]}";

                ProfileImage.SaveAs(HttpContext.Current.Server.MapPath($"~/images/userimg/{filename}"));
                model.ProfileImageFileName = filename;

            }
        }

        public static bool ResumePDFUpload(HttpPostedFileBase ResumeFile)
        {
            if (ResumeFile != null && (ResumeFile.ContentType == "application/pdf"))
            {
                string filename = $"user_{Guid.NewGuid()}.{ResumeFile.ContentType.Split('/')[1]}";

                ResumeFile.SaveAs(HttpContext.Current.Server.MapPath($"~/Files/Upload/{filename}"));
                return true;
            }

            return false;
        }

        public static bool ResumeAjaxUpload(HttpPostedFileBase ResumeFile)
        {
            if(Directory.Exists(HttpContext.Current.Server.MapPath("~/Files/Upload"))==false)
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Files/Upload"));
            }

            if (ResumeFile != null && (ResumeFile.ContentType == "application/pdf"))
            {
                //ResumeFile.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath("~/Files/Upload"), ResumeFile.FileName)); //Combine ile path ile dosya adini birleştirir.Full path yapar Combine.
                string filename = $"user_{Guid.NewGuid()}.{ResumeFile.ContentType.Split('/')[1]}"; //Biz gelen dosyanın adını değiştirip Guid atamayı tercih ettik.

                ResumeFile.SaveAs(HttpContext.Current.Server.MapPath($"~/Files/Upload/{filename}"));
                return true;
            }

            return false;
        }

    }
}