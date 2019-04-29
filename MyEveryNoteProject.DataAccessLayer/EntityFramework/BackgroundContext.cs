using MyEveryNoteProject.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEveryNoteProject.DataAccessLayer.EntityFramework
{
    public class BackgroundContext : DbContext
    {
        public DbSet<Log> Db_Logs { get; set; }

        public BackgroundContext()
        {
            Database.SetInitializer(new LogInitializer());
        }
    }
}
