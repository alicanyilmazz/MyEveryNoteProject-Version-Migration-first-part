using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEveryNoteProject.Entities.ValueObjects
{
    public class ContactFormViewModel
    {
        [DisplayName("Name"), Required, StringLength(25), MinLength(5), MaxLength(20)]
        public string Name { get; set; }

        [DisplayName("Email"), Required, StringLength(50), EmailAddress(), RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail type is not valid"), MinLength(5), MaxLength(50)]
        public string Email { get; set; }

        [DisplayName("Subject"), Required, StringLength(25), MinLength(5), MaxLength(20)]
        public string Subject { get; set; }

        [DisplayName("Message"), Required, StringLength(250), MinLength(5), MaxLength(225)]
        public string Message { get; set; }

    }
}
