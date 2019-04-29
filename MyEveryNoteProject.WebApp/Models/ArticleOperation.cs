using MyEveryNoteProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEveryNoteProject.WebApp.Models
{
    public class ArticleOperation
    {
        public List<Comment> data_comment_list { get; set; }
        public Article data_article { get; set; }
       
    }
}