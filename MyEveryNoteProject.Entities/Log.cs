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
    [Table("Logs")]
    public class Log
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required,DisplayName("Process Date")]
        public DateTime ProcessDate { get; set; }

        [Required,StringLength(100),DisplayName("Username")]
        public string Username { get; set; }

        [StringLength(100),DisplayName("Action")]
        public string ActionName { get; set; }
        
        [StringLength(100),DisplayName("Controller")]
        public string ControllerName { get; set; }

        [StringLength(100),DisplayName("Information")]
        public string Information { get; set; }

        [StringLength(200), DisplayName("ExceptionInformation")]
        public string ExceptionInformation { get; set; }
    }
}
