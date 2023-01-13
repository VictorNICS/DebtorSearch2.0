using DebtorSearch.Business_Objects;
using DebtorSearch.DatabaseReader;
using DebtorSearch.Implementations;
using DebtorSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;


namespace DebtorSearch.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly SearchHistoryRepository searchHistory = new SearchHistoryRepository();
        // GET: Reports
        [HttpGet]
        public ActionResult Home()
        {
           
            var emails = NICS.System.Security.Utils.AdUser.GetAllEmails();
            ReportsViewModel report = new ReportsViewModel();
            if (DataBaseReader.GetUserRoleByEmail().Department =="16")
            {
                report.Clients = DataBaseReader.SupervisorBooks();
            }
            else
            {
                report.Clients = DataBaseReader.Clients();
            }
           
            report.ReportTypes = DataBaseReader.ReportTypes();
            return View(report);
        }
        [HttpPost]
        public ActionResult Home(ReportsViewModel model)
        {
            model.Clients = DataBaseReader.Clients();
            model.ReportTypes = DataBaseReader.ReportTypes();
            var searchXml = NICS.System.Data.Serialization.GetXml<ReportsViewModel>(model);
            var logSearchHistory = new SearchHistory
            {
                SearchLog = searchXml,
                UserName = HttpContext.User.Identity.Name,
                MachineName = Environment.MachineName,
                WindowsUserName = Environment.UserName,
                SystemType = 2

            };
            searchHistory.Add(logSearchHistory);
            searchHistory.Save();
       
            model.Clients = DataBaseReader.Clients();
            model.ReportTypes = DataBaseReader.ReportTypes();

            if (model.Report == 1)
            {

                if (model.Client == "SARS")
                {
                    var db = DataBaseReader.ViewCamapaignReportSars(model.Client);
                    NICS.System.Data.Serialization.ExportToExcel2(db.Tables[0], model.Client + DateTime.Now.ToString("yyyyMMdd"));
                    TempData["Message"] = "Campaign Report for  " + model.Client + " Generated succefully";
                    TempData["Icon"] = "success";
                }
                else
                {
                    var db = DataBaseReader.ViewCamapaignReport(model.Client);
                    NICS.System.Data.Serialization.ExportToExcel2(db.Tables[0], model.Client + "CamapaignReport" + DateTime.Now.ToString("yyyyMMdd"));
                    TempData["Message"] = "Campaign Report for  " + model.Client + " Generated succefully";
                    TempData["Icon"] = "success";
                }
               
                

            }
            if (model.Report ==2)
            {
                var db = DataBaseReader.ViewAllClientsInfoReport(model.Client);
                NICS.System.Data.Serialization.ExportToExcel2(db.Tables[0], model.Client + "InfoReport" + DateTime.Now.ToString("yyyyMMdd"));
                TempData["Message"] = "Client Info Report for  " + model.Client + " Generated succefully";
                TempData["Icon"] = "success";
               
            }
          
        
            //return RedirectToAction("Index","Search");
            return View(model);
        }

        //protected override void Dispose(bool disposing)
        //{
          
        //    if (disposing)
        //    {
        //        if (searchHistory != null)
        //        {
        //            searchHistory.Dispose();
        //        }
        //    }

        //    base.Dispose(disposing);
        //}
    }
}