using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEveryNoteProject.DataAccessLayer.EntityFramework
{
    public class RepositoryBaseLog
    {
        protected static BackgroundContext db;
        private static object _lockSync = new object();

        protected RepositoryBaseLog()  // class ın new lenmemesi için instance olusturulamaması için protected yaptık.Bundan ancak miras alan nesne yaratabilir.
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
                        db = new BackgroundContext();
                    }

                }
            }

        }
    }
}
