using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEveryNoteProject.Entities.FeedBacks
{
    public class UserDeletionReason
    {
        public int reasonId { get; set; }
        public string reason { get; set; }

        //public int UserId { get; set; }

        public static List<UserDeletionReason> GetUserDeletionReasons()
        {
            return new List<UserDeletionReason>()
            {
                new UserDeletionReason(){reasonId=1,reason="I don't understand how to use the site."},
                 new UserDeletionReason(){reasonId=2,reason="I have more than one account."},
                  new UserDeletionReason(){reasonId=3,reason="I don't have enough time"},
                   new UserDeletionReason(){reasonId=4,reason="Other"}

            };
        }
    }
}
