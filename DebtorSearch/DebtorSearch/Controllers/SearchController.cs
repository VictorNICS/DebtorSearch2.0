using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DebtorSearch.Business_Objects;
using DebtorSearch.DatabaseReader;
using DebtorSearch.Implementations;
using DebtorSearch.Models;
using PagedList;

namespace DebtorSearch.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private readonly SearchHistoryRepository _searchHistory = new SearchHistoryRepository();
        private readonly UserRepository _user = new UserRepository();
        private readonly BooksRepository _book = new BooksRepository();

        public ActionResult Index()
        {
  
            return View();
        }
        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(DebtorSearchViewModel model)
        {
            if (!model.Empty)
            {
                model.DebtorSearchViewModelList = null;
                var db = DataBaseReader.GetDebtors(model).ToList();
                var searchXml = NICS.System.Data.Serialization.GetXml<DebtorSearchViewModel>(model);
                var logSearchHistory = new SearchHistory
                {
                    SearchLog = searchXml,
                    UserName = HttpContext.User.Identity.Name,
                    MachineName = Environment.MachineName,
                    WindowsUserName = Environment.UserName,
                    SystemType = 1

                };
                _searchHistory.Add(logSearchHistory);
                _searchHistory.Save();

                // var result = db.GroupBy(t => new { t.ContactNo, t.AccountNumber }).Select(g => g.First()).ToList();
                var result = db.GroupBy(t =>t.AccountNumber ).Select(g => g.First()).ToList();
                if (result.Any())
                {
                    model.DebtorSearchViewModelList = result;

                }
                TempData["Message"] = result.Count() + " record(s) where found!!";
                TempData["Icon"] = "info";
            }
            else
            {
                TempData["Message"] ="Search Criteria cannot be empty!!";
                TempData["Icon"] = "error";
            }
           
            return View(model);
        }

        public ActionResult Maintain(int? Page_No)
        {
            var userList = new List<UserViewModel>();

            var users = _user.GetAll().ToList();

            foreach (var user in users)
            {
                var u = new UserViewModel
                {
                    Id =user.Id,
                   Department = DataBaseReader.PopulateRoles().Where(t=>t.Value ==user.Department).First().Text,
                   Name = user.Name,
                   Surname =user.Surname,
                   EmployeeNo = user.EmployeeNo,
                   SecurityQuestion =user.SecurityQuestion,
                   UserName = user.UserName,
                   SecurityAnswer = user.SecurityAnswer
                };
                userList.Add(u);
            }
            return View(userList);

        }
        [System.Web.Mvc.HttpGet]
        public ActionResult Update(string id)
        {
          
            if (id !=null )
            {
                var updateUser = _user.FindBy(t => t.Id == id.ToString()).FirstOrDefault();
               
                if (updateUser != null)
                {
                    ViewBag.Times = DataBaseReader.PopulateRoles().ToList();
                    var user = new RegisterViewModel {
                        Id =updateUser.Id,
                        Email =updateUser.Email,
                        Department =updateUser.Department,
                        Surname = updateUser.Surname,
                        Name =updateUser.Name,
                        EmployeeNo = updateUser.EmployeeNo,
                        SecurityQuestion = updateUser.SecurityQuestion,
                        SecurityAnswer = updateUser.SecurityAnswer,
                        
                    };
                    return View(user);
                }
                
            }
            return RedirectToAction("Index", "Search");
        
        }
        [HttpGet]
        public ActionResult MaintainSupervisors()
        {
            var supervisorList = new List<BooksViewmodel>();
            var users = _user.GetAll().Where(t => t.Department == "16");

            if (users != null)
            {
               
                foreach (var user in users)
                {
                    var books = _book.FindBy(t => t.UserName == user.UserName).ToList();
                    var sList = new BooksViewmodel
                    {
                        Id = user.Id,
                        Surname =user.Surname,
                        Name =user.Name,
                        Books = books,
                        UserName = user.UserName
                        
                    };
                    supervisorList.Add(sList);

                }
            }

            return View(supervisorList);

        }
        [HttpGet]
        public ActionResult AddSupervisorBooks(string id )
        {
            var user = _user.FindBy(t => t.Id == id).FirstOrDefault();
            var supervisor = new BooksViewmodel();
            supervisor.Clients = DataBaseReader.Clients();
            if (user != null)
            {
                var books = _book.FindBy(t => t.UserName == user.UserName).ToList();

                supervisor.Books = books;
                supervisor.UserName = user.UserName;
            }
            
            return View(supervisor);

        }
        [HttpPost]
        public ActionResult AddSupervisorBooks(BooksViewmodel model)
        {
            if (model.BookName != null)
            {
                var alreadyExist = _book.FindBy(t => t.BookName == model.BookName && t.UserName == model.UserName).ToList();
                if (alreadyExist.Count > 0)
                {
                    TempData["Message"] = " Already Exists!!";
                    TempData["Icon"] = "info";
                }
                else
                {
                    var book = new Books
                    {
                        UserName = model.UserName,
                        BookName = model.BookName
                    };

                    _book.Add(book);
                    _book.Save();
                    TempData["Message"] = " Successfully Added!!";
                    TempData["Icon"] = "success";

                    var mail = new MailMessage();
                    mail.To.Add(model.UserName);
                    mail.CC.Add("it@nics.co.za");
                    mail.From = new MailAddress("no-reply@nics.co.za");
                    mail.Subject = "Adding Book";
                    mail.Priority = MailPriority.High;
                    mail.IsBodyHtml = true;
                    mail.Body = "This is to notify you that the book: " + model.BookName + " has been added to your name.";
                    NICS.System.Notifications.Email.Sendmail(mail);

                }


            }
    
            return RedirectToAction("Index","Search");

}

        public ActionResult Remove(string id)
        {
          
            if (id != null)
            {
                var book = _book.FindBy(t=>t.Id.ToString() == id).FirstOrDefault();
                if (book != null)
                {


                    var mail = new MailMessage();
                    mail.To.Add(book.UserName);
                    mail.CC.Add("it@nics.co.za");
                    mail.From = new MailAddress("no-reply@nics.co.za");
                    mail.Subject = "Removing Book";
                    mail.Priority = MailPriority.High;
                    mail.IsBodyHtml = true;
                    mail.Body = "This is to notify you that the book: " + book.BookName + " has been removed on your name.";
                    NICS.System.Notifications.Email.Sendmail(mail);


                    _book.Remove(book);
                    _book.Save();
                    TempData["Message"] = " Successfully Removed!!";
                    TempData["Icon"] = "success";

                }
            }
            return RedirectToAction("Index","Search");
           

        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Update(RegisterViewModel model)
        {
                var User = _user.FindBy(t => t.Id == model.Id).FirstOrDefault();
                if (User != null)
                {
                var Department = DataBaseReader.PopulateRoles().Where(t => t.Value == model.Department).First().Text;
                    User.Department = model.Department;
                    _user.Update(User);
                    _user.Save();
                TempData["Message"] = "User "+ model.Email  + " Successfully updated!!";
                TempData["Icon"] = "success";

                var itUser = _user.FindBy(t => t.UserName == HttpContext.User.Identity.Name).FirstOrDefault();

                var mail = new MailMessage();
                mail.To.Add(User.Email);
                mail.CC.Add("it@nics.co.za");
                mail.From = new MailAddress("no-reply@nics.co.za");
                mail.Subject = "Role Change";
                mail.Priority = MailPriority.High;
                mail.IsBodyHtml = true;
                mail.Body = "This is to notify you that your role has been changed to " + Department + " by " + itUser.Surname + " " + itUser.Name;
                NICS.System.Notifications.Email.Sendmail(mail);

            }
          
            return RedirectToAction("Index", "Search");

        }

        public ActionResult Delete(string id)
        {
            var User = _user.FindBy(t => t.Id == id).FirstOrDefault();
            if (User != null)
            {

                _user.Remove(User);
                _user.Save();
                TempData["Message"] = "User " + User.Email + " Successfully Removed!!";
                TempData["Icon"] = "success";

            }

            return RedirectToAction("Index", "Search");

        }

        public JsonResult GetUsers(string Prefix)
        {
            var users = _user.FindBy(t => t.Name.StartsWith(Prefix));
            return Json(users, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_searchHistory != null)
                {
                    _searchHistory.Dispose();
                }

            }

            base.Dispose(disposing);
        }

    }
}