using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyEveryNoteProject.Entities;

namespace MyEveryNoteProject.WebApp.Models
{
    public class DataOperationManager
    {

        public List<Article> data_article_list { get; set; }

        public int number_of_elements { get; set; }
        public int categoryid { get; set; }
        public bool likecntrl { get; set; }

        public static int TakePageNumber(int num)
        {

            int pagenum, cntr1;
            float cntrl2;
            if (num > 9)
            {

                cntr1 = (num / 9);
                cntrl2 = ((float)num / 9);
                if (cntrl2 > cntr1)
                {
                    pagenum = cntr1 + 1;
                }
                else
                {
                    pagenum = cntr1;
                }

                return pagenum;
            }

            pagenum = 1;
            return pagenum;
        }
    }
}