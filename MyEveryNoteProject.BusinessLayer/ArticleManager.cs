using MyEveryNoteProject.BusinessLayer.Abstract;
using MyEveryNoteProject.DataAccessLayer.EntityFramework;
using MyEveryNoteProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEveryNoteProject.BusinessLayer
{
    public class ArticleManager : ManagerBase<Article>
    {
       

    }
}


/*
  Ornegin,

        public List<Article> GetAllArticles()
        {
            return List();
        }

    *Metodu bulunmaktaydı bu ve buna benzer Specific isler yapmayan methodları sildik cunku zaten ManagerBase aracılıgıyla erişilen Repository class ında 
    * List donduren methodlar bulunmakta.
    * Ancak EveryNoteUserManager daki gibi farklı sorugulama ve filtreleme islemi gerektiren methodlara ihtiyac olursa örnegin SignIn,SignUp vb.
    * Bu metodlarda yine temelde Repository nin methodlarını kullanıyor ama bununla birlikte mail atma hata donme gibi ekstra islemleri de onları yaptıgından silmedik.
    * Yani kısaca Manager class larda temel database işlemleri yapılmaz zaten bunlar Repository de yapılıyor ekstra özel methodlara ihtiyaç duyarsak ilgili Manager 
    * classlara metodumuzu yazarız.

*/
