using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DebtorSearch.Models
{
    public class NewPasswordViewModel
    {
     
        [Display(Name ="Old Password")]
        public string OldPassword { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }



    }
}