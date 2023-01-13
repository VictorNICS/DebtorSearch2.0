using DebtorSearch.Business_Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DebtorSearch.Models
{
    public class BooksViewmodel
    {
        public string Id     { get; set; }
        
        public string Name { get; set; }
        public string Surname { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "Book")]
        public string BookName { get; set; }
        public virtual List<Books> Books { get; set; }
        public List<SelectListItem> Clients { get; set; }
    }
}