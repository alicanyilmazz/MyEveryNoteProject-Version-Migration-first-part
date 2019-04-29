using MyEveryNoteProject.DataAccessLayer.EntityFramework;  //Refactoring sayesinde yarın baska bir veritabanı veya ORM aracı sectiginde using MyEveryNoteProject.DataAccessLayer.MySql; der o folder içerindekileri kullandırırsın Test.cs yi değiştirmeden.
using MyEveryNoteProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*Test class ı sayesinde Database olusumu ve tum Repository methodlarının dogru çalısıp calısmadıgı test edilmistir.*/
namespace MyEveryNoteProject.BusinessLayer
{
    public class Test
    {
        private Repository<EveryNoteUser> repo_user = new Repository<EveryNoteUser>();
        private Repository<Category> repo_category = new Repository<Category>();
        private Repository<Comment> repo_comment = new Repository<Comment>();
        private Repository<Article> repo_article = new Repository<Article>();
        private Repository<Liked> repo_likes = new Repository<Liked>();


        public Test()
        {
            List<Category> categories = repo_category.List();
            //List<Category> categories_filtered = repo_category.List(x=>x.Id>5);
        }

        /*User Insert,Update,Delete testing methods (boylece iliskili baska tablolarla iliski gerektirmeyen tabloya insert örengi yaptık.)*/
        public void InsertTest()
        {

            int result = repo_user.Insert(new EveryNoteUser()
            {
                Name = "aaa",
                Surname = "bbb",
                Email = "cemalhunal@hunal.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "aabb",
                Password = "1111",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifierUser = "aabb"
            });
        }
        public void UpdateTest()
        {
            EveryNoteUser user_test = repo_user.Find(x => x.Username == "user7");

            if (user_test != null)
            {
                user_test.Username = "sultansuleyman";
                int result = repo_user.Update(user_test);
            }
        }
        public void DeleteTest()
        {
            EveryNoteUser user_test = repo_user.Find(x => x.Username == "sultansuleyman");

            if (user_test != null)
            {
                int result = repo_user.Delete(user_test);

            }

        }

        public void CommentTest() //Hatırlarsak Comment Article ile iliskili id hangi article ın yorumu ve kim bu yorumu birakti.İliskili tablolarda insert update delete nasıl onun testing ini yapıyoruz.
        {
            EveryNoteUser user_test = repo_user.Find(x => x.Id == 2);
            Article article_test = repo_article.Find(x => x.Id == 3);

            Comment comment_test = new Comment()
            {
                Text = "Bu bir test mesajidir",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                ModifierUser = "tancakemal",
                Articles = article_test,   //Bu articla  bu commenti.
                Owner = user_test         //Bu kullanıcı ekliyor.

            };

            repo_comment.Insert(comment_test);
        }

    }
}

/*
 * Acıklamalarım *
1)
Suna dikkat edelim Repository Generic sınıfından hangi class türünden nesne olusturursak artık o nesneye ait Repository methodkarı o class türünden calisacak.
Dolaysıyla o nesne Category tipindense insert metodu Category insert ederken, Comment tipinden ise insert metodu Comment insert edecek.

2)
Eskiden ,
public Test()
{
    DataAccessLayer.DatabaseContext db = new DataAccessLayer.DatabaseContext();  //Artık DatabaseContext new lemiyoruz bunu artık bundan sonra Repository yapacak.
    db.Dbset_Categories.ToList();



}
seklinde DatabaseContext den bir nesne yaratıyorduk artık bunu yukarda gördügünüz gibi Repository yapıyor.Buna dikkat edelim.

3)
  public void CommentTest() metodunda,
  Comment in insert edilebilmesi için bu commentin hangi article a ve hangi kullanıcı tarafından eklendiğinin belirtilmesi gerekiyor.
  EveryNoteUser user_test = repo_user.Find(x => x.Id == 2);
  Article article_test = repo_article.Find(x => x.Id == 3); ile gerekli sorguları yapıyoruz ki bunlarda ,
  en tepedeki,

     private Repository<EveryNoteUser> repo_user = new Repository<EveryNoteUser>();
     private Repository<Article> repo_article = new Repository<Article>();   nesnelerini kullanıyor fakat bu nesnelerin herbiri olustugundan Database context nesnesi yaratılıyordu.

    ve .Net bize su hatayı veriyor user ile sorgu atarken kullandıgın DatabaseContext nesnen farklı Article ile sorgu atarken kullandıgın DatabaseContext nesnen farklı,
    bunları bunları ilişkilendirirken hangi kulannıcının hangi article ına yorum yapılmıs ,

     Articles = article_test,   //Bu articla  bu commenti.
     Owner = user_test         //Bu kullanıcı ekliyor.

    bunların DatabaseContext nesnleri birbirinden farklı iken birde 
    Comment comment_test = new Comment() {} i kullanırken bir daha bir DatabaseContext nesnesi yaratıyorsun comment_test nesnesininde DatabaseContext nesnesi farklı.
    2 Farklı DatabaseContext nesnesi içeren nesne ile sorgu yapıp yine Farklı DatabaseContext nesnesi içeren comment_test nesnesi ile bunu insert etmeye calısıyorsun bu nedenle IEntityChangeTracker hatası aldık.
    Bunu önlemenin yolu ise hangisinden 

        private Repository<EveryNoteUser> repo_user = new Repository<EveryNoteUser>();
        private Repository<Category> repo_category = new Repository<Category>();
        private Repository<Comment> repo_comment = new Repository<Comment>();
        private Repository<Article> repo_article = new Repository<Article>();
        private Repository<Liked> repo_likes = new Repository<Liked>();

    olusturulan hangi nesne kullanılsa kullanılsın hepsi için tek bir DatabaseContext nesnesi yaratılmalı bunun içinde Singleton Pattern kullandık.



 */
