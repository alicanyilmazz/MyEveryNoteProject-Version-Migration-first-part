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
    [Table("EveryNoteUsers")]
    public class EveryNoteUser : MyEntityBase
    {
        [DisplayName("Name"), Required, StringLength(25)]
        public string Name { get; set; }

        [DisplayName("Surname"), Required, StringLength(25)]
        public string Surname { get; set; }

        [DisplayName("Username"), Required, StringLength(25)]
        public string Username { get; set; }

        [DisplayName("Email"), Required, StringLength(70)]
        public string Email { get; set; }

        [DisplayName("Password"), Required, StringLength(350)]
        public string Password { get; set; }

        [StringLength(270),ScaffoldColumn(false)]
        public string ProfileImageFileName { get; set; }

        [DisplayName("Is Active"),Required]
        public bool IsActive { get; set; } //kullanıcının aktive olup olmadıgını kontrol etmek için kullanıyoruz.(boolean ında varsayılan hali bos gecilemezdir fakat biz yine okuma acısından Required yazıyoruz.)

        [Required,ScaffoldColumn(false)]
        public Guid ActivateGuid { get; set; } //Kullanıcıya hesabını aktive etmesi için gönderdiğimiz Guid ile veritabanından o kişiyi bulacağız.(Guid in StringLenght i olmaz)

        [DisplayName("Is Admin"), Required]
        public bool IsAdmin { get; set; } //Kullanıcı Admin mi
        public virtual List<Article> Articles { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Liked> Likes { get; set; }

    }
}
