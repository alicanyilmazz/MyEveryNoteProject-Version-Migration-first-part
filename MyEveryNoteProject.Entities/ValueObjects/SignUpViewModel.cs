using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyEveryNoteProject.Entities.ValueObjects
{
    public class SignUpViewModel
    {
        [DisplayName("Name"), Required, StringLength(25), MinLength(2), MaxLength(20)]
        public string Name { get; set; }

        [DisplayName("Surname"), Required, StringLength(25), MinLength(2), MaxLength(20)]
        public string Surname { get; set; }

        [DisplayName("Username"), Required, StringLength(25), MinLength(5), MaxLength(20)]
        public string UserName { get; set; }

        [DisplayName("Email"), Required, StringLength(50), EmailAddress(), RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail type is not valid"), MinLength(5), MaxLength(50)]
        public string Email { get; set; }

        [DisplayName("Password"), Required, StringLength(25), DataType(DataType.Password), RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^\\da-zA-Z])(.{8,15})$", ErrorMessage = "Minimum 8 Max 15 characters atleast 1 Alphabet, 1 Number and 1 Special Character and avoid space"),MinLength(8),MaxLength(15)]
        public string Password { get; set; }

        [DisplayName("Repassword"), Required, StringLength(25), DataType(DataType.Password), Compare("Password")]
        public string Repassword { get; set; }

        //[DisplayName("Street Name"), Required, StringLength(25)]
        //public string StreetName { get; set; }

        //[DisplayName("Street Number"), Required, StringLength(25)]
        //public string StreetNumber { get; set; }

        //[DisplayName("City"), Required, StringLength(25)]
        //public string City { get; set; }

        //[DisplayName("Country"), Required, StringLength(25)]
        //public string Country { get; set; }
    }
}