using MyEveryNoteProject.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Singleton Pattern Uygulaması
namespace MyEveryNoteProject.DataAccessLayer.EntityFramework
{
    public class RepositoryBase
    {
        protected static DatabaseContext db;
        private static object _lockSync = new object();

        protected RepositoryBase()  // class ın new lenmemesi için instance olusturulamaması için protected yaptık.Bundan ancak miras alan nesne yaratabilir.
        {
            CreateContext();
        }
        private static void CreateContext() // Geriye DatabaseContext nesnesi dönen CreateContext() metodumuz.Artık _db ile nesne yaratabilen CreateContext() metodudur.Ama metodda static oldugundan nesne üzeinden erişilemez o halde artık nesne yaratamayacagız.
        {
            if (db == null)
            {
                lock (_lockSync)
                {
                    if (db == null)
                    {
                        db = new DatabaseContext();
                    }

                }
            }

        }

    }
}
//CreateContext() metodunu static yapma sebebimiz ne peki?Cunku RepositoryBase class ının Constructor ını protected yaptık ve bu sınıftan kalıtım almayan bu sınıfdan nesne yaratamaz.
//Ama nesne yaratamıyorsak Constructor dan dolayı nasıl CreateContext() metoduna erişeceğiz probleminide static metod olarak tanımlayarak cözdük.
//O halde CreateContext() metoduna direkt classadi.CreateContext() şeklinde erişebileceğiz.
// Lock() kullanılmasının nedeni ise Multithread uygulamalar var ve aynı anda 2 thread
/*
 
 if(_db==null)
 {
     _db = new DatabaseContext();
 }
     
 ayna bu if statement içerisine girebiliyor ve iki kere new leme (nesne yaratılmasına neden olabiliyor.). Bu gibi durumları kontrol etmek içinde Lock() denilen yapı ile (using bloguna benzer) kilitleme yapılır.
 Lock(){}  aynı anda iki thread in calistirilmasını engeller.
 Lock() bizden bir nesne ister parametre olarak bir tane nesne tanımladık object türünden sadece suna dikkat bu tanımladıgımız nesneninde static olmasına dikkat edelim çünkü static method içerisinde kullanıyoruz o yuzden kendiside static olmalı.

2.Adımda ise yukarıdaki tüm yapıyı degistirdik direk Repository yi RepositoryBase den kalıtım aldırdık.
Boylece Artık direkt Repository den db ye ulasım imkanı kazandı.
Nesne yaratıldıgı anda otomatik olarak direkt DatabaseContext nesneside yaratılacak.(Constructor sayesinde tabi yine bir kere)

    private static DatabaseContext CreateContext() özelliğinide kaldırdım.Geriye deger dondurmesine Guncelleme ile gerek kalmadı.
    {}

    Eski hali
    ///////////////////////////////////////////////////////////////

        private static DatabaseContext CreateContext() // Geriye DatabaseContext nesnesi dönen CreateContext() metodumuz.Artık _db ile nesne yaratabilen CreateContext() metodudur.Ama metodda static oldugundan nesne üzeinden erişilemez o halde artık nesne yaratamayacagız.
        {
            if (db == null)
            {
                lock (_lockSync)
                {
                    if (db == null)
                    {
                        db = new DatabaseContext();
                    }

                }
            }

            return db;
        }
    
    /////////////////////////////////////////////////////////////
*/
