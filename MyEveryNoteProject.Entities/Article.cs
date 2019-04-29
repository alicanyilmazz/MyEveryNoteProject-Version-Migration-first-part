using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEveryNoteProject.Entities
{
    [Table("Articles")]
    public class Article : MyEntityBase
    {
        [DisplayName("Article Title"),Required,StringLength(60)]
        public string Title { get; set; }

        [DisplayName("Article Text"),Required, StringLength(2000)]
        public string Text { get; set; }

        [DisplayName("Draft"),Required]
        public bool IsDraft { get; set; } //Daha bu yazı taslak mı?Yayına almadanda düzenleme imkanı

        [DisplayName("Likes"),Required]
        public int LikeCount { get; set; }  // int ve bool ifadeler zaten otomatik olarak bos gecilemezdir fakat biz yinede Required olarak belirttik.

        [DisplayName("Article Image"),StringLength(270)]
        public string ArticleImageFileName { get; set; }

        [Required]
        public int CategoryId { get; set; } // [ilişkili oldugu tablonun adi+ilişkili oldugu property nin adi] Bu bize ilişkili tablolarda tabloya diğer ilişkide oldugu tablonun Id sinin otomatik eklenmesini sağlar.
        //Ayrıca Category Id yi ögrenmek için ekstra bir sorgu yapmanıza gerek kalmayacak direkt Article üzerine yaptıgınız sorguda kaydın Category Id si gelecek zaten.
        public virtual EveryNoteUser Owner { get; set; } //Makelenin sahibi
        public virtual List<Comment> Comments { get; set; }
        public virtual Category Categories { get; set; }
        public virtual List<Liked> Likes { get; set; }

        //public virtual List<Category> Categories { get; set; }

        public Article()
        {
            Comments = new List<Comment>();
            Likes = new List<Liked>();
        }

    }
}
