using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEveryNoteProject.Entities
{
    [Table("Comments")]
    public class Comment:MyEntityBase
    {  
        [Required,StringLength(300)]
        public string Text { get; set; } //yorumlar
        //public bool IsApproval { get; set; }

        public virtual Article Articles { get; set; }
        public virtual EveryNoteUser Owner { get; set; }
    }
}
