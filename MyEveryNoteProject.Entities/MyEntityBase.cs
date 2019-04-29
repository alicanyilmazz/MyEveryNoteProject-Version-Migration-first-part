using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Bu sınıf her tabloda ortak olarak kullanılacak prop ları tutmak için kullanılmıstır.
namespace MyEveryNoteProject.Entities
{
    [Table("MyEntityBases")]
    public class MyEntityBase
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; } //Creation time  (Datetime zaten null geçilemez typedır fakat biz yine Required yazdık. )

        [Required]
        public DateTime ModifiedOn { get; set; } //Modified time

        [Required,StringLength(30)]
        public string ModifierUser { get; set; } //EveryNoteUser ile ilişkisel bir bag kurmamıza gerek olmadıgından string type ı verdik.
    }
}
