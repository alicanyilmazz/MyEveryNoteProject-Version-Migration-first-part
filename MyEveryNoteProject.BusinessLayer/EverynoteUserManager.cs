using MyEveryNoteProject.BusinessLayer.Abstract;
using MyEveryNoteProject.BusinessLayer.Results;
using MyEveryNoteProject.Common.Helpers;
using MyEveryNoteProject.DataAccessLayer.EntityFramework;
using MyEveryNoteProject.Entities;
using MyEveryNoteProject.Entities.Messages;
using MyEveryNoteProject.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEveryNoteProject.BusinessLayer
{
    public class EverynoteUserManager : ManagerBase<EveryNoteUser>
    {

        BusinessLayerResult<EveryNoteUser> LayerResult = new BusinessLayerResult<EveryNoteUser>();
        public BusinessLayerResult<EveryNoteUser> SignUpUser(SignUpViewModel data)
        {

            data.Password = CryptoHelper.Manager(data.Password);

            EveryNoteUser user = Find(x => x.Username == data.UserName || x.Email == data.Email);

            if (user != null)
            {
                if (user.Username == data.UserName)
                {
                    LayerResult.AddErrorMessage(ErrorMessageCode.UsernameAlreadyExist, "Username already exist.");
                }
                if (user.Email == data.Email)
                {
                    LayerResult.AddErrorMessage(ErrorMessageCode.EmailAlreadyExist, "Email already exist.");
                }
            }
            else
            {
                int dbResult = base.Insert(new EveryNoteUser()
                {
                    Username = data.UserName,
                    Email = data.Email,
                    Password = data.Password,
                    ActivateGuid = Guid.NewGuid(),
                    ProfileImageFileName = "defaultuser.png",
                    Name = data.Name,
                    Surname = data.Surname,
                    IsActive = false,
                    IsAdmin = false

                });

                if (dbResult > 0)
                {
                    LayerResult.Result = Find(x => x.Email == data.Email && x.Username == data.UserName);
                    string siteurl = ConfigHelper.Get<string>("SiteRootUrl");
                    string activateurl = $"{siteurl}/Home/UserActivate/{LayerResult.Result.ActivateGuid}";
                    string messagebody = $"Hi {LayerResult.Result.Name} {LayerResult.Result.Surname};<br/><br/> plase click for activated your <a href='{activateurl}' target='_blank'>account</a>.";
                    MailHelper.SendMail(messagebody, LayerResult.Result.Email, "UNWEAS active your account.");

                }
            }

            return LayerResult;

        }

        public BusinessLayerResult<EveryNoteUser> SignInUser(SignInViewModel data)
        {
            data.Password = CryptoHelper.Manager(data.Password);

            LayerResult.Result = Find(x => x.Username == data.Username && x.Password == data.Password);

            if (LayerResult.Result != null)
            {
                if (!LayerResult.Result.IsActive)
                {
                    LayerResult.AddErrorMessage(ErrorMessageCode.UserIsNotActive, "User not activated.");
                    LayerResult.AddErrorMessage(ErrorMessageCode.CheckYourEmail, "Plase check your email.");
                }
            }
            else
            {
                LayerResult.AddErrorMessage(ErrorMessageCode.UsernameOrPaswordNotMatch, "Username or password does not match.");
            }

            return LayerResult;
        }

        public BusinessLayerResult<EveryNoteUser> ActivateUser(Guid activateId)
        {

            LayerResult.Result = Find(x => x.ActivateGuid == activateId);

            if (LayerResult.Result != null)
            {
                if (LayerResult.Result.IsActive)
                {
                    LayerResult.AddErrorMessage(ErrorMessageCode.UserAlreadyActive, "User already active!");

                    return LayerResult;
                }

                LayerResult.Result.IsActive = true;
                Update(LayerResult.Result);
            }
            else
            {
                LayerResult.AddErrorMessage(ErrorMessageCode.ActivateIdDoesNotExist, "No user to activate.");
            }

            return LayerResult;
        }

        public BusinessLayerResult<EveryNoteUser> GetUserById(int id)
        {
            LayerResult.Result = Find(x => x.Id == id);

            if (LayerResult.Result == null)
            {
                LayerResult.AddErrorMessage(ErrorMessageCode.UserNotFound, "User not Found!");
            }

            return LayerResult;
        }

        public BusinessLayerResult<EveryNoteUser> UpdateProfile(EveryNoteUser data)
        {
            data.Password = CryptoHelper.Manager(data.Password);
            //EveryNoteUser db_user = Find(x => x.Username == data.Username || x.Email == data.Email); //Kullanıcının yeni sectiği username veya email ile database de eslesen kayıt varmı ona bakacağız ki kullanıcı baskasının kaydını değiştiremesin.
            EveryNoteUser db_user = Find(x => x.Id != data.Id && (x.Username == data.Username || x.Email == data.Email));

            if (db_user != null && db_user.Id != data.Id)
            {
                if (db_user.Username == data.Username)
                {
                    LayerResult.AddErrorMessage(ErrorMessageCode.UsernameAlreadyExist, "Username already exist!");
                }
                if (db_user.Email == data.Email)
                {
                    LayerResult.AddErrorMessage(ErrorMessageCode.EmailAlreadyExist, "Email already exist!");
                }

                return LayerResult;
            }

            LayerResult.Result = Find(x => x.Id == data.Id);
            LayerResult.Result.Email = data.Email;
            LayerResult.Result.Username = data.Username;
            LayerResult.Result.Name = data.Name;
            LayerResult.Result.Surname = data.Surname;
            LayerResult.Result.Password = data.Password;
            LayerResult.Result.ModifierUser = data.Username;
            LayerResult.Result.ModifiedOn = DateTime.Now;


            if (string.IsNullOrEmpty(data.ProfileImageFileName) == false)
            {
                LayerResult.Result.ProfileImageFileName = data.ProfileImageFileName;
            }

            if (base.Update(LayerResult.Result) == 0)
            {
                LayerResult.AddErrorMessage(ErrorMessageCode.ProfileNotUpdated, "Profile not update!");
            }

            return LayerResult;
        }

        public BusinessLayerResult<EveryNoteUser> RemoveUserById(int id)
        {
            EveryNoteUser user = Find(x => x.Id == id);

            if (user != null)
            {
                if (Delete(user) == 0)
                {
                    LayerResult.AddErrorMessage(ErrorMessageCode.UserCouldNotRemove, "User could not remove!");
                    return LayerResult;
                }
            }
            else
            {
                LayerResult.AddErrorMessage(ErrorMessageCode.UserCouldNotFind, "User could not find!");
            }

            return LayerResult;
        }

        public BusinessLayerResult<EveryNoteUser> ProfileDeletionControl(int id, string username_validation, string password_validation)
        {
            LayerResult.Result = Find(x => x.Id == id);

            if (LayerResult.Result == null)
            {
                LayerResult.AddErrorMessage(ErrorMessageCode.UserNotFound, "User not Found!");
            }
            else if (LayerResult.Result != null)
            {
                if (LayerResult.Result.Username != username_validation || LayerResult.Result.Password != password_validation)
                {
                    LayerResult.AddErrorMessage(ErrorMessageCode.ProfileDeletionVerifyInfoWrong, "Your account can not delete because your password or username is wrong!");
                }
            }
            return LayerResult;
        }

        //Method Hiding for Insert
        public new BusinessLayerResult<EveryNoteUser> Insert(EveryNoteUser data)
        {
            EveryNoteUser user = Find(x => x.Username == data.Username || x.Email == data.Email);
            LayerResult.Result = data;

            if (user != null)
            {
                if (user.Username == data.Username)
                {
                    LayerResult.AddErrorMessage(ErrorMessageCode.UsernameAlreadyExist, "Username already exist.");
                }
                if (user.Email == data.Email)
                {
                    LayerResult.AddErrorMessage(ErrorMessageCode.EmailAlreadyExist, "Email already exist.");
                }
            }
            else
            {

                LayerResult.Result.ProfileImageFileName = "defaultuser.png";
                LayerResult.Result.ActivateGuid = Guid.NewGuid();

                if (base.Insert(LayerResult.Result) == 0)
                {
                    LayerResult.AddErrorMessage(ErrorMessageCode.UserCouldNotInserted, "User could not inserted.");
                }


            }

            return LayerResult;

        }

        public new BusinessLayerResult<EveryNoteUser> Update(EveryNoteUser data)
        {
            /*EveryNoteUser db_user = Find(x => x.Username == data.Username || x.Email == data.Email);*/ //Kullanıcının yeni sectiği username veya email ile database de eslesen kayıt varmı ona bakacağız ki kullanıcı baskasının kaydını değiştiremesin.
            EveryNoteUser db_user = Find(x => x.Id != data.Id && (x.Username == data.Username || x.Email == data.Email));
            LayerResult.Result = data;

            if (db_user != null && db_user.Id != data.Id)
            {
                if (db_user.Username == data.Username)
                {
                    LayerResult.AddErrorMessage(ErrorMessageCode.UsernameAlreadyExist, "Username already exist!");
                }
                if (db_user.Email == data.Email)
                {
                    LayerResult.AddErrorMessage(ErrorMessageCode.EmailAlreadyExist, "Email already exist!");
                }

                return LayerResult;
            }

            LayerResult.Result = Find(x => x.Id == data.Id);
            LayerResult.Result.Email = data.Email;
            LayerResult.Result.Username = data.Username;
            LayerResult.Result.Name = data.Name;
            LayerResult.Result.Surname = data.Surname;
            LayerResult.Result.Password = data.Password;
            //LayerResult.Result.ModifierUser = data.Username;
            LayerResult.Result.ModifiedOn = DateTime.Now;
            LayerResult.Result.IsActive = data.IsActive;
            LayerResult.Result.IsAdmin = data.IsAdmin;


            //if (string.IsNullOrEmpty(data.ProfileImageFileName) == false)   //Admin user resimlerini düzenleyemez!
            //{
            //    LayerResult.Result.ProfileImageFileName = data.ProfileImageFileName;
            //}

            if (base.Update(LayerResult.Result) == 0)
            {
                LayerResult.AddErrorMessage(ErrorMessageCode.UserCouldNotUpdated, "User not updated!");
            }

            return LayerResult;
        }

        public bool ContactForm(string contact_name, string contact_email, string contact_subject, string message_body)
        {
            string sending_mail_address = "alicanyilmaz101@gmail.com";
            string messagebody = $"Message Subject : {contact_subject}<br/>Contact Name : {contact_name} <br/> Contact Email Address : {contact_email} <br/><br/> Message : {message_body}";
            bool mail_res = MailHelper.SendMail(messagebody, sending_mail_address, contact_subject, true);

            if (mail_res == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}


/*
 * Acıklama 1:
 *  önceden biz " private Repository<EveryNoteUser> repo_user = new Repository<EveryNoteUser>(); "
 *  Gibi her Manager Class içerisinde DataAccessLayer içerisindeki Repository 'e ulasmak icin o sınıfın tipini vererek Cunku Repository Class ı generic idi
 *  Repository e ait methodları (Insert,Find...) gibi kullanıyorduk.Ama artık her Manager class da teker teker 
 * 
 *      private Repository<EveryNoteUser> repo_user = new Repository<EveryNoteUser>();
 *      private Repository<Article> repo_article = new Repository<Article>();
 *      private Repository<Category> repo_category = new Repository<Category>();
 *  
 *  gibi tek tek Repository den nesne yaratıp Repository e erişmek yerine 
 *  
 *  İlgili Manager sınıfa  : ManagerBase<Category>  ManagerBase'den kalıtım aldırdıgımızda onda bulunan generic,
 *  
 *     private Repository<T> repo = new Repository<T>();
 *     yapısı sayesinde otomatikman ilgili repo_(user veya category veya article) nesnesi otomatikman repo ismi ile olusturuluyor
 *     
 *          
 */

/*
 *Acıklama 2:
 * 
 * Dikkat ederseniz Method hiding işlemi yaotık "new" anahtar kelimesi ile.Anlamı su base class da da aynı isimli bir method varsa ki "Insert" methodu
 * ManagerBase de bulunmakta işte biz aynı isimli method olusturarak base class daki değil de bizim yazdıgımız türetilmiş sınıftaki aynı isimli methodun kullanılmasını
 * saglamıs olduk.Yani , "public new BusinessLayerResult<EveryNoteUser> Insert(EveryNoteUser obj)" methodu base classdaki Insert methodunun yerine gecmiş oluyor.
 * Biliyorsunuz ki Interfacedeki methodları virtual yapıp onları ezme yeteneğine override özelliği kazandırmıstık.Fakat bize lazım olan ,
 * BusinessLayerResult<EveryNoteUser> tipinden değer dönmesi methodumuzun ki hataları yakalayabilelim, çünkü overiding de geri dönüş tipini değiştiremeyeceğimizden
 * sorun çıkacaktı.
     
  **Önemli peki Base class daki Insert methodunu kullanan yerlerde derleyici hangi Insert i kullanması gerektiğini nasıl bilebilir sorusunun cevabı ise,
  * base.Insert() şeklinde Base class a ait Insert kullanılacaksa o zaman basına "base. " seklinde bir ifade kullanmalıyız.
     
     
*/
