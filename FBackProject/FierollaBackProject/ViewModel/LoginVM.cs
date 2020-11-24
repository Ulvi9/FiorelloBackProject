using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PartialViewHomeWork.ViewModel
{
    public class LoginVM
    {
        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set;}
       
        //[Required,StringLength(100)]
        //public string Username { get; set; }
        //[Required,DataType(DataType.Password)]
        //public string Password { get; set; }
        //public bool RememberMe { get; set; }
        //public string ReturnUrl { get; set; }
    }
}
