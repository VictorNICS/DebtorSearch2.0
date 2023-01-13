using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DebtorSearch.Models
{
    public class ActivityReportModel
    {
        public string Client { get; set; }
        public List<SelectListItem> Clients { get; set; }
    }

    public class ActivityReportsStructureModel
    {
        public DataTable TotalPTP { get; set; }
        public DataTable ClientName { get; set; }
    }
}