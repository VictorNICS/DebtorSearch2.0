using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DebtorSearch.Controllers
{
    public class SmsController : Controller
    {
        // GET: Sms
        public ActionResult Index(FormCollection collection)
        {
            var option = collection["Auto"];

            if (option == null)
            {
                ViewBag.Options = "false";
            }
            else
            {
                ViewBag.Options = option;
            }

            var smsReport = DatabaseReader.DataBaseReader.GetSMSReports().Tables[0];
            ViewBag.LastUpdateTime = DateTime.Now;
            return View(smsReport);
        }
    }
}