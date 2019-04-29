using MyEveryNoteProject.BusinessLayer;
using MyEveryNoteProject.BusinessLayer.Results;
using MyEveryNoteProject.Entities;
using MyEveryNoteProject.Entities.FeedBacks;
using MyEveryNoteProject.Entities.Messages;
using MyEveryNoteProject.Entities.ValueObjects;
using MyEveryNoteProject.WebApp.Filters;
using MyEveryNoteProject.WebApp.Models;
using MyEveryNoteProject.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyEveryNoteProject.WebApp.Controllers
{
    //[ExcFilter]
    public class HomeController : Controller
    {
        private ArticleManager article_mngr = new ArticleManager();
        private CategoryManager category_mngr = new CategoryManager();
        private EverynoteUserManager everynoteuser_mngr = new EverynoteUserManager();
        private LogManager log_mngr = new LogManager();

        [ExcFilter]
        [HttpGet]
        public ActionResult Index()
        {
            /* return View(article_mngr.ListQueryable().OrderByDescending(x => x.ModifiedOn).ToList());*/ //Yukarıdaki kodda ListQueryable() türünden alıp OrderBy ıda ekleyip .ToList() dediğimizde (ensonda) OrderBy lı Sql sorgusu olusturacak bize.
                                                                                                          /* return View(article_mngr.GetAllArticles().OrderByDescending(x => x.ModifiedOn).ToList()); *///IEnumerable  Sorgu ile yapımı (C# tarafında)
                                                                                                                                                                                                         /* return View(article_mngr.GetQueryableArticles().OrderByDescending(x => x.ModifiedOn).ToList()); *///IQueryable   Sorgu ile yapımı (Sql tarafında)

            ////log_mngr.List(); //ikinci database in olusmasi icin gecici olarak kullanıldı.
            //return View(article_mngr.ListQueryable().Where(x => x.IsDraft == false).OrderByDescending(x => x.ModifiedOn).ToList());


            //int num = article_mngr.ListQueryable().Where(x => x.IsDraft == false).Count();
            //ViewBag.paggingnumber = PaggingManager.TakePageNumber(num);

            //return View(article_mngr.ListQueryable().Where(x => x.IsDraft == false).OrderByDescending(x => x.ModifiedOn).Take(9).ToList());

            return View();
        }

        //[HttpGet]
        //public PartialViewResult TakeAricleBody(int? id=0)
        //{
        //    int skp = id.Value;

        //    if(id.Value!=0)
        //    {
        //        skp = (id.Value - 1) * 9;
        //    }

        //    List<Article> articles=article_mngr.ListQueryable().Where(x => x.IsDraft == false).OrderByDescending(x => x.ModifiedOn).Skip(skp).Take(9).ToList();

        //    DataOperationManager dat = new DataOperationManager();
        //    dat.data_article_list = articles;
        //    dat.number_of_elements = DataOperationManager.TakePageNumber(article_mngr.ListQueryable().Where(x => x.IsDraft == false).Count());


        //    return PartialView("_PartialIndexBody", dat);
        //}

        [ExcFilter]
        [HttpGet]
        public PartialViewResult TakeArticleBody(int? datpagnum = 0, int datcatnum = 0, bool likecntrl = false)
        {

            DataOperationManager dat = new DataOperationManager();
            int skp = datpagnum.Value;

            if (datpagnum.Value != 0)
            {
                skp = (datpagnum.Value - 1) * 9;
            }


            if (datcatnum == 0 && datpagnum != 0 && likecntrl == false)
            {
                List<Article> articles = article_mngr.ListQueryable().Where(x => x.IsDraft == false).OrderByDescending(x => x.ModifiedOn).Skip(skp).Take(9).ToList();

                dat.data_article_list = articles;
                dat.number_of_elements = DataOperationManager.TakePageNumber(article_mngr.ListQueryable().Where(x => x.IsDraft == false).Count());
                dat.categoryid = 0;
                dat.likecntrl = false;

                return PartialView("_PartialIndexBody", dat);
            }
            else if (datcatnum != 0 && datpagnum == 0 && likecntrl == false)
            {
                List<Article> categorizedarticles = article_mngr.ListQueryable().Where(x => x.IsDraft == false && x.CategoryId == datcatnum).OrderByDescending(x => x.ModifiedOn).Take(9).ToList();
                dat.data_article_list = categorizedarticles;
                dat.number_of_elements = DataOperationManager.TakePageNumber(article_mngr.ListQueryable().Where(x => x.IsDraft == false && x.CategoryId == datcatnum).Count());
                dat.categoryid = datcatnum;
                dat.likecntrl = false;

                return PartialView("_PartialIndexBody", dat);
            }
            else if (datcatnum != 0 && datpagnum != 0 && likecntrl == false)
            {
                List<Article> categorizedarticles = article_mngr.ListQueryable().Where(x => x.IsDraft == false && x.CategoryId == datcatnum).OrderByDescending(x => x.ModifiedOn).Skip(skp).Take(9).ToList();
                dat.data_article_list = categorizedarticles;
                dat.number_of_elements = DataOperationManager.TakePageNumber(article_mngr.ListQueryable().Where(x => x.IsDraft == false && x.CategoryId == datcatnum).Count());
                dat.categoryid = datcatnum;
                dat.likecntrl = false;
                return PartialView("_PartialIndexBody", dat);
            }
            else if (datcatnum == 0 && datpagnum == 0 && likecntrl == true)
            {
                List<Article> likestatusarticle = article_mngr.ListQueryable().Where(x => x.IsDraft == false).OrderByDescending(x => x.LikeCount).Take(9).ToList();
                dat.data_article_list = likestatusarticle;
                dat.number_of_elements = DataOperationManager.TakePageNumber(article_mngr.ListQueryable().Where(x => x.IsDraft == false).OrderByDescending(x => x.LikeCount).Count()); // TODO : zaten yine üsteki ile aynı sayısı döner işi otomatikleştir.
                dat.categoryid = 0;
                dat.likecntrl = true;
                return PartialView("_PartialIndexBody", dat);
            }
            else if (datcatnum == 0 && datpagnum != 0 && likecntrl == true)
            {
                List<Article> likestatusarticle = article_mngr.ListQueryable().Where(x => x.IsDraft == false).OrderByDescending(x => x.LikeCount).Skip(skp).Take(9).ToList();
                dat.data_article_list = likestatusarticle;
                dat.number_of_elements = DataOperationManager.TakePageNumber(article_mngr.ListQueryable().Where(x => x.IsDraft == false).OrderByDescending(x => x.LikeCount).Count()); // TODO : zaten yine üsteki ile aynı sayısı döner işi otomatikleştir.
                dat.categoryid = 0;
                dat.likecntrl = true;
                return PartialView("_PartialIndexBody", dat);
            }
            //Aslinda asagidaki durum catid==0 && id==0 durumunu ifade etmekdir. fakat .Net bize en az bir return ifadesini if-else statement dişinda yazmaya zorladiğindan bunu yukaridaki şartlar sağlanmadiği takdirde garanti girilecek durum olarak aşagi yazdik!

            List<Article> get_articles = article_mngr.ListQueryable().Where(x => x.IsDraft == false).OrderByDescending(x => x.ModifiedOn).Take(9).ToList();


            dat.data_article_list = get_articles;
            dat.number_of_elements = DataOperationManager.TakePageNumber(article_mngr.ListQueryable().Where(x => x.IsDraft == false).Count());
            dat.categoryid = 0;
            dat.likecntrl = false;
            return PartialView("_PartialIndexBody", dat);
        }


        //[HttpGet]
        //public PartialViewResult TakeCategoryFilteredArticle(int? id)
        //{
        //    List<Article> articles = article_mngr.ListQueryable().Where(x => x.IsDraft == false && x.CategoryId == id).OrderByDescending(x => x.ModifiedOn).ToList();
        //    DataOperationManager dat = new DataOperationManager();
        //    dat.data_article_list = articles;
        //    dat.number_of_elements = DataOperationManager.TakePageNumber(article_mngr.ListQueryable().Where(x => x.IsDraft == false && x.CategoryId == id).Count());


        //    return PartialView("_PartialIndexBody", dat);
        //}


        //[HttpGet]
        //public ActionResult Category(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    //ESKİ YONTEM//
        //    //  Category cat = category_mngr.Find(x => x.Id == id.Value); 

        //    //if (cat == null)  //Category Silinmis olabilir.  
        //    //{
        //    //    //return HttpNotFound();
        //    //    return RedirectToAction("Index", "Home");
        //    //}
        //    //List<Article> articles = cat.Articles.Where(x => x.IsDraft == false).OrderByDescending(x => x.ModifiedOn).ToList();

        //    //return View("Index",articles);
        //    //ESKİ YONTEM//

        //    List<Article> articles = article_mngr.ListQueryable().Where(x => x.IsDraft == false && x.CategoryId == id).OrderByDescending(x => x.ModifiedOn).ToList();

        //    return View("Index", articles);

        //}


        //[HttpGet]
        //public ActionResult MostLiked()
        //{
        //    return View("Index", article_mngr.ListQueryable().OrderByDescending(x => x.LikeCount).ToList());
        //}

        [ExcFilter]
        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Status = TempData["data"];
            ViewBag.StatusMessage = TempData["TempMessage"];
            // TODO : Buraya en çok okunan 4 yazar getirilecek!
            // TODO : Percentages Of Articles By Category hesaplanacak

            return View();
        }

        [ExcFilter]
        [HttpGet]
        public ActionResult SignIn()
        {


            return View();
        }

        [ExcFilter]
        [HttpPost]
        public ActionResult SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<EveryNoteUser> res = everynoteuser_mngr.SignInUser(model);

                if (res.Errors.Count > 0)
                {

                    if (res.Errors.Find(x => x.Code == ErrorMessageCode.UserIsNotActive) != null)
                    {
                        ViewBag.SetLink = "http://Home/Activate/12365-8925-56565";
                    }


                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message)); //Tüm Errors List inde Foreach ile dön herbiri için ilgili string i (x yani) ModelState in Error üne ekle.

                    return View(model);
                }

                CurrentSession.Set<EveryNoteUser>("SignIn", res.Result);  // TODO :  Yonlendirme yada Session a kullanıcı bilgisi atılacak
                return RedirectToAction("Index");
            }


            return View(model);
        }

        [ExcFilter]
        [HttpGet]
        public ActionResult SignUp()
        {


            return View();
        }

        [ExcFilter]
        [HttpPost]
        public ActionResult SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid) //Gelen verilerin Modeldeki propertylere uygunlugunu kontrol eder burada modelimiz 'SignUpViewModel' dir.
            {

                BusinessLayerResult<EveryNoteUser> res = everynoteuser_mngr.SignUpUser(model);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message)); //Tüm Errors List inde Foreach ile dön herbiri için ilgili string i (x yani) ModelState in Error üne ekle.
                    return View(model);
                }

                UploadManager.UserProfileImageFolderCreate(res.Result.Id);

                SuccessfulOperationsViewModel successful_notifyobj = new SuccessfulOperationsViewModel()
                {
                    title = "Your registration is successful.",
                    text = "Please activate your account to sign in.",
                    RedirectingUrl = "/Home/SignIn"
                };
                successful_notifyobj.Items.Add("Please activate your account using the activation link we sent to you.");

                return View("SuccessfulOperation", successful_notifyobj);

            }
            return View(model);
        }

        [ExcFilter]
        [HttpGet]
        public ActionResult UserActivate(Guid id)
        {
            BusinessLayerResult<EveryNoteUser> res = everynoteuser_mngr.ActivateUser(id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel error_notifyobj = new ErrorViewModel()
                {
                    title = "Invalid Operation!",
                    text = "Your process was rejected by the system!",
                    Items = res.Errors
                };

                return View("Error", error_notifyobj);
            }

            SuccessfulOperationsViewModel successful_notifyobj = new SuccessfulOperationsViewModel()
            {
                title = "Your activation is successful.",
                text = "Please login to the system using your user information.",
                RedirectingUrl = "/Home/SignIn"
            };
            successful_notifyobj.Items.Add("If your activation mail does not arrive you can use the help menu.");
            return View("SuccessfulOperation", successful_notifyobj);
        }

        [ExcFilter]
        [Auth]
        [ActFilter]
        [ResFilter]
        [HttpGet]
        public ActionResult ShowProfile()
        {
            BusinessLayerResult<EveryNoteUser> res = everynoteuser_mngr.GetUserById(CurrentSession.User.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel error_notifyobj = new ErrorViewModel()
                {
                    title = "Hoops!Something went wrong.",
                    text = "Requested operation could not be performed!",
                    Items = res.Errors
                };

                return View("Error", error_notifyobj);
            }

            return View(res.Result);
        }

        [ExcFilter]
        [Auth]
        [ActFilter]
        [ResFilter]
        [HttpGet]
        public ActionResult EditProfile()
        {

            BusinessLayerResult<EveryNoteUser> res = everynoteuser_mngr.GetUserById(CurrentSession.User.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel error_notifyobj = new ErrorViewModel()
                {
                    title = "Hoops!Something went wrong.",
                    text = "Requested operation could not be performed!",
                    Items = res.Errors
                };

                return View("Error", error_notifyobj);
            }
            res.Result.Password = null;
            return View(res.Result);
        }

        [ExcFilter]
        [Auth]
        [ActFilter]
        [ResFilter]
        [HttpPost]
        public ActionResult EditProfile(EveryNoteUser model, HttpPostedFileBase ProfileImage)
        {
            //ModelState.Remove("ModifierUser");

            if (ModelState.IsValid)
            {
                UploadManager.UserUpdateImageInsert(model, ProfileImage);

                BusinessLayerResult<EveryNoteUser> res = everynoteuser_mngr.UpdateProfile(model);

                if (res.Errors.Count > 0)
                {
                    ErrorViewModel error_notifyobj = new ErrorViewModel()
                    {
                        Items = res.Errors,
                        title = "Profile not update!",
                        RedirectingUrl = "/Home/EditProfile",
                        RedirectionMessage = "You are being redirected to the EditProfile page."

                    };

                    return View("Error", error_notifyobj);
                }

                //Profil güncellendiği için Session güncellendi!
                CurrentSession.Set<EveryNoteUser>("SignIn", res.Result);

                return RedirectToAction("ShowProfile");

            }

            return View(model);
        }

        [ExcFilter]
        [Auth]
        [ActFilter]
        [ResFilter]
        [HttpGet]
        public ActionResult RemoveProfile()
        {

            BusinessLayerResult<EveryNoteUser> res = everynoteuser_mngr.RemoveUserById(CurrentSession.User.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel error_notifyobj = new ErrorViewModel()
                {
                    Items = res.Errors,
                    title = "Your profile did not delete!",
                    text = "Please contact the management!",
                    RedirectingUrl = "/Home/ShowProfile"
                };

                return View("Error", error_notifyobj);
            }

            Session.Clear();

            //return RedirectToAction("Index");

            SuccessfulOperationsViewModel successful_notifyobj = new SuccessfulOperationsViewModel()
            {
                title = "Your account has been deleted.",
                text = "If you accidentally deleted your account, please contact us.",
                RedirectingUrl = "/Home/Index"
            };
            successful_notifyobj.Items.Add("If you do not make a request within 30 days, your account will be permanently deleted.");

            return View("SuccessfulOperation", successful_notifyobj);
        }

        [ExcFilter]
        [Auth]
        [ActFilter]
        [ResFilter]
        [HttpGet]
        public ActionResult RemoveProfileFeedBack()
        {

            BusinessLayerResult<EveryNoteUser> res = everynoteuser_mngr.GetUserById(CurrentSession.User.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel error_notifyobj = new ErrorViewModel()
                {
                    Items = res.Errors,
                    title = "Your profile did not delete!",
                    text = "Please contact the management!",
                    RedirectingUrl = "/Home/EditProfile"
                };

                return View("Error", error_notifyobj);
            }

            DeletionFeedBackViewModel dfb_vm = new DeletionFeedBackViewModel()
            {
                username = CurrentSession.User.Username,
                ProfileImageFileName = CurrentSession.User.ProfileImageFileName,
                SelectedReasonId = 0,
                SL_DeletionReasons = new SelectList(UserDeletionReason.GetUserDeletionReasons(), "ReasonId", "Reason"),

            };

            return View(dfb_vm);
        }

        [ExcFilter]
        [Auth]
        [ActFilter]
        [ResFilter]
        [HttpPost]
        public ActionResult RemoveProfileFeedBack(DeletionFeedBackViewModel model)
        {

            BusinessLayerResult<EveryNoteUser> res = everynoteuser_mngr.ProfileDeletionControl(CurrentSession.User.Id, model.username_validation, model.password_validation);

            if (res.Errors.Count > 0)
            {

                DeletionFeedBackViewModel mdl = new DeletionFeedBackViewModel()
                {
                    ProfileImageFileName = CurrentSession.User.ProfileImageFileName,
                    username = CurrentSession.User.Username,
                    message_validation = model.message_validation,
                    password_validation = model.password_validation,
                    username_validation = model.username_validation,
                    SelectedReasonId = model.SelectedReasonId,
                    SL_DeletionReasons = new SelectList(UserDeletionReason.GetUserDeletionReasons(), "ReasonId", "Reason")

                };
                res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                return View(mdl);
            }

            return RedirectToAction("RemoveProfile");
        }

        [ExcFilter]
        [HttpGet]
        public ActionResult SignOut()
        {
            Session.Clear();

            return RedirectToAction("Index");
        }

        [ExcFilter]
        [HttpGet]
        public ActionResult TestNotify()
        {
            SuccessfulOperationsViewModel model1 = new SuccessfulOperationsViewModel()
            {
                Header = "INFORMATION",
                title = "Operation Successful.",
                Items = new List<string>() { "Test basarili 1 ", "Test Basarili 2" },
                IsRedirecting = false
            };

            InformationOperationsViewModel model2 = new InformationOperationsViewModel()
            {
                Header = "INFORMATION",
                title = "Operation Successful.",
                Items = new List<string>() { "Test basarili 1 ", "Test Basarili 2" },
                IsRedirecting = false
            };

            WarningOperationsViewModel model3 = new WarningOperationsViewModel()
            {
                Header = "INFORMATION",
                title = "Operation Successful.",
                Items = new List<string>() { "Test basarili 1 ", "Test Basarili 2" },
                IsRedirecting = false
            };

            ErrorViewModel model4 = new ErrorViewModel()
            {
                Header = "INFORMATION",
                title = "Operation Successful.",
                Items = new List<ErrorMessageObject>()
                {
                    new ErrorMessageObject(){Message="Test Hatasi Bildirimi 1"},
                    new ErrorMessageObject(){Message="Test Hatasi Bildirimi 2"}
                },
                IsRedirecting = false
            };

            return View("Error", model4);
        }

        [ExcFilter]
        [HttpGet]
        public ActionResult AccessDenied()
        {
            ErrorViewModel error_notifyobj = new ErrorViewModel()
            {
                Header = "WARNING",
                title = "Hoops!Access Denied.",
                Items = new List<ErrorMessageObject>()
                {
                    new ErrorMessageObject(){Message="You are not authorized to access this domain!"},
                    new ErrorMessageObject(){Message="Requested operation could not be performed!"}
                }

            };

            return View("Error", error_notifyobj);
        }

        [ExcFilter]
        public ActionResult ExceptionHandling()
        {
            if (TempData["CatchedException"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Exception model = TempData["CatchedException"] as Exception;

            return View(model);
        }

        [ExcFilter]
        public FilePathResult SiteInformationPDF()
        {
            string file_path = Server.MapPath("~/Files/Download/Pdf/siteinformation.pdf");

            return new FilePathResult(file_path, "application/pdf");
        }

        //[HttpPost]
        //public ActionResult ResumeUpload(HttpPostedFileBase UploadedFile)  //Ajax siz (-)
        //{
        //    bool result = UploadManager.ResumePDFUpload(UploadedFile);

        //    TempData["data"] = result;
        //    if(result==true)
        //    {
        //        TempData["TempMessage"] = "Your upload has been successful!";
        //    }
        //    else
        //    {
        //        TempData["TempMessage"]= "Your upload has not been successful!";
        //    }

        //    return RedirectToAction("About");
        //}

        [ExcFilter]
        [HttpPost]
        public JsonResult ResumeUpload(HttpPostedFileBase ResumeUploadFiles)  // Ajax ile (+)
        {
            bool rslt = UploadManager.ResumeAjaxUpload(ResumeUploadFiles);

            //TempData["data"] = rslt; //Bunlar Ajax kullanılmayan önceki versiyonda bilgi amaçlı Helper method için kullanılıyordu artık gerek kalmadı yeni versiyon ile Ajax içerisinde bildirimler verilecektir.
            if (rslt == true)
            {
                //TempData["TempMessage"] = "Your upload has been successful!";

                return Json(new { hasError = false, message = "Your resume upload has been successful!" });
            }
            else
            {
                //TempData["TempMessage"] = "Your upload has not been successful!";

                return Json(new { hasError = true, message = "Your resume upload has not been successful!" });
            }


        }

        [ExcFilter]
        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Status = TempData["data"];
            ViewBag.StatusMessage = TempData["TempMessage"];
            return View();
        }

        //[HttpPost]
        //public ActionResult Contact(ContactFormViewModel model)  //Ajax siz (-)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        bool result = everynoteuser_mngr.ContactForm(model.Name, model.Email, model.Subject, model.Message);

        //        TempData["data"] = result;

        //        if (result == true)
        //        {
        //            TempData["TempMessage"] = "Your message has been sended!";
        //        }
        //        else
        //        {
        //            TempData["TempMessage"] = "Your message has not been sended!";
        //        }

        //        return View();
        //    }

        //    return View(model);
        //}

        [ExcFilter]
        [HttpPost]
        public JsonResult ContactFormUpload(ContactFormViewModel product)    //Ajax ile (+)
        {
            if (ModelState.IsValid)
            {
                bool result = everynoteuser_mngr.ContactForm(product.Name, product.Email, product.Subject, product.Message);

                if (result == true)
                {
                    return Json(new { hasError = false, message = "Your message sended has been successful!" });
                }
                else
                {
                    return Json(new { hasError = true, message = "Your message did not send as a successful!" });
                }

            }

            string messages = string.Join("  ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));


            return Json(new { hasError = true, message = messages });

        }

        [ExcFilter]
        public ActionResult ArticleWholePage(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Article article = article_mngr.Find(x => x.Id == id);
           
            if (article == null)
            {
                return HttpNotFound();
            }

            ViewBag.ArticleID = id;

            return View("ArticleWholePage",article);
           
        }

        [ExcFilter]
       
        public PartialViewResult ArticleWholePagePartialView(int? id)
        {
            if (id == null)
            {
               
            }

            Article article = article_mngr.ListQueryable().Include("Comments").FirstOrDefault(x => x.Id == id);
            

            return PartialView("_PartialArticleWholePage", article.Comments);
           

        }
    }
}