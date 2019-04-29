using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEveryNoteProject.Entities
{
    [Table("Likes")]
    public class Liked
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } //MyEntityBase den kalıtım almadık cunku bize sadece Id özelliği lazım.
        public virtual Article Articles { get; set; }
        public virtual EveryNoteUser LikedUser { get; set; }
    }
}
