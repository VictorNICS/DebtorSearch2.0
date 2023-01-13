using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DebtorSearch.Models
{
    public class ConnStringViewModel
    {
        public int Id { get; set; }

     
        [Display(Name="Database Name:")]
        public string Name { get; set; }

        [Display(Name = "Server:")]
        public string ServerName { get; set; }

        [Display(Name = "User Id:")]
        public string UserId { get; set; }
       
        public string Password { get; set; }
      
        public string ConString { get; set; }
        public List<SelectListItem> Databases { get; set; }
    

    }
}