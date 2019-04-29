using MyEveryNoteProject.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEveryNoteProject.DataAccessLayer.EntityFramework
{
    class LogInitializer: CreateDatabaseIfNotExists<BackgroundContext>
    {
        protected override void Seed(BackgroundContext context)
        {
            Log log_process = new Log()
            {
                ActionName="ActionDeneme",
                ControllerName="ControllerDeneme",
                Username="03alican",
                ProcessDate=DateTime.Now,
                Information="bu bir denemedir.",
                ExceptionInformation="Exception olusmadi"
            };

            context.Db_Logs.Add(log_process);
            context.SaveChanges();
        }
    }
}
