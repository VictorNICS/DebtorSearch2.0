using DebtorSearch.Business_Objects;
using DebtorSearch.Common;
using DebtorSearch.Implementations;
using DebtorSearch.Models;
using NICS.System.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DebtorSearch.Controllers
{
    public class ActivityController : Controller
    {
       private readonly ConnectionStringsRepository con = new ConnectionStringsRepository();
        // GET:Activity
        public ActionResult Home() {
            var model = new ActivityReportModel
            {
                Clients = DatabaseReader.DataBaseReader.SourceList()
            };
            return View(model);

        }

        // POST: Activity
        [HttpPost]
        public ActionResult Index(ActivityReportModel model)
        {
            

            if (model != null)
            {

                ViewBag.LastUpdateTime = DateTime.Now;
                MYSQL.con = model.Client;


                var actvity = new ActivityReportsStructureModel
                {
                    TotalPTP = DatabaseReader.MySqlClass.GetConsultantActivityReport(),
                    ClientName = DatabaseReader.MySqlClass.ClientName()

                };
           
               // var table = actvity.TotalPTP.DataSet.Tables[];
                return View(actvity);
            }
            else
            {
                return View("Home");
            }
            
            //var actvity = DatabaseReader.MySqlClass.GetConsultantActivityReport();

          
          
          
        }

        [HttpGet]
        public ActionResult Create()
        {
            MYSQL.con = "Server=192.168.0.16;Database=information_schema;Uid=lexiir;Pwd=L3x!1Ru$eR; Allow User Variables=True";

            var model = new ConnStringViewModel
            {

                Databases = DatabaseReader.MySqlClass.GetTableNames()

            };
        
           
            return View(model);

        }

        [HttpPost]
        public ActionResult Create(ConnStringViewModel model)
        {
           // Server = 192.168.0.16; Database = eks; Uid = lexiir; Pwd = L3x!1Ru$eR; Allow User Variables = True"
            if (model != null)
            {
                var bookExists = con.FindBy(t => t.Name == model.Name).ToList().Count();
                model.Databases = DatabaseReader.MySqlClass.GetTableNames();
                if (bookExists > 0)
                {
                    TempData["Message"] = "Book already Exists!!";
                    TempData["Icon"] = "error";
                }
                else
                {
                    var c = new ConnectionStrings
                    {
                        Name = model.Name,
                        ServerName = model.ServerName,
                        UserId = model.UserId,
                        Password = model.Password,
                       // ConString = "Server=" + model.ServerName + ";Database=" + model.Name + ";Uid=" + model.UserId + ";Pwd=" + model.Password + ";Allow User Variables=True"

                    };
                    con.Add(c);
                    con.Save();
                    TempData["Message"] = "Added Successfully!!";
                    TempData["Icon"] = "success";

                    return RedirectToAction("Index", "Search");
                }
            }

        

            return View(model);

        }

        [HttpGet]

        public ActionResult UpdatePassword()
        {
          

            return View();
        }

        [HttpPost]

        public ActionResult UpdatePassword(NewPasswordViewModel model)
        {
            if (model != null)
            {
                using (NicsDebtorSearch db = new NicsDebtorSearch())
                {

                   var rowsAffected= db.Database.ExecuteSqlCommand("UPDATE ConnectionStrings SET Password = {0} WHERE Password = {1}", model.Password, model.OldPassword);
                    if (rowsAffected > 0)
                    {
                        TempData["Message"] = "SuccessFully Updated!!";
                        TempData["Icon"] = "success";

                    }
                    else
                    {
                        TempData["Message"] = "!!";
                        TempData["Icon"] = "error";
                    }
                }
                
                return RedirectToAction("Index", "Search");

            }

            return View();

        }
        [HttpGet]
        public ActionResult DeleteConnectionString() {
            //List<object> conStrings = new List<object>();
            var conStrings = new List<ConnStringViewModel>();
          var connections =con.GetAll().ToList();

            foreach (var item in connections)
            {
                var lstConStrings = new ConnStringViewModel
                {
                    Name = item.Name,
                    Id = item.Id,
                    ConString = item.ConString,
                    ServerName = item.ServerName,
                    UserId = item.UserId
                    
                };
                conStrings.Add(lstConStrings);
            }
            
            return View(conStrings);
        }
   
        public ActionResult Delete(int id)
        {
           // var integer = int.Parse(id);
            var conString = con.FindBy(t => t.Id == id).FirstOrDefault();
            if (conString != null)
            {
                con.Remove(conString);
                con.Save();
                TempData["Message"] = "SuccessFully deleted!!";
                TempData["Icon"] = "success";
                return RedirectToAction("Index", "Search");
            }
            else
            {
                TempData["Message"] = "Not Found!!";
                TempData["Icon"] = "error";
            }
            return View();
        }


    }
 


}