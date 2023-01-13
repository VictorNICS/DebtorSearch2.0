using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DebtorSearch.Models
{
    public class ReportsViewModel
    {
        [Display(Name = "Report Type")]
        public int Report { get; set; }
        [Display(Name = "Book")]
        public string Client { get; set; }
        public List<SelectListItem> ReportTypes { get; set; }
        public List<SelectListItem> Clients { get; set; }
    }
}