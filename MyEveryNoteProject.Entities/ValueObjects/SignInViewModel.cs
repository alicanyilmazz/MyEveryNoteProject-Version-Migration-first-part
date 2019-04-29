using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyEveryNoteProject.Entities.ValueObjects
{
    public class SignInViewModel
    {
        [DisplayName("Username"),Required,StringLength(25), MinLength(5), MaxLength(20)]
        public string Username { get; set; }

        [DisplayName("Password"), Required, StringLength(25),DataType(DataType.Password), RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^\\da-zA-Z])(.{8,15})$", ErrorMessage = "Minimum 8 Max 15 characters atleast 1 Alphabet, 1 Number and 1 Special Character and avoid space"), MinLength(8), MaxLength(15)]
        public string Password { get; set; }
    }
}