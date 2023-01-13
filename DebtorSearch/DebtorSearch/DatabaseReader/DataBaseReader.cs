using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NICS.System.Data;
using DebtorSearch.Business_Objects;
using DebtorSearch.Models;
using System.Linq.Expressions;
using DebtorSearch.Implementations;

namespace DebtorSearch.DatabaseReader
{
   

    public static class DataBaseReader
    {
       static readonly  UserRepository userRepository = new UserRepository();
        public static IQueryable<TSource> DistinctBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
        {
            return source.GroupBy(keySelector).Select(x => x.FirstOrDefault());
        }
        static ClientsData Context = new ClientsData();
        //private static ClientsData Context;
        //public DataBaseReader()
        //{
        //    Context = new ClientsData();
        //}
        public static RecordSet ViewAllClientsInfoReport(string ClientName)
        {
            var oParams = new DBParamCollection
            {
                {"@ClientName", ClientName }

            };
            return new RecordSet("[dbo].[ViewAllClientInforReport]", QueryType.StoredProcedure, oParams);
        }

        public static RecordSet ViewCamapaignReport(string ClientName)
        {
            var oParams = new DBParamCollection
            {
                {"@ClientName", ClientName }

            };
            return new RecordSet("[dbo].[ViewAllCampaignReport]", QueryType.StoredProcedure, oParams);
        }

        public static RecordSet GetSMSReports()
        {

            var con = System.Configuration.ConfigurationManager.ConnectionStrings["Sms"].ConnectionString;
            var oParams = new DBParamCollection
            {
               

            };
            return new RecordSet("[dbo].[GetSMSReports]", QueryType.StoredProcedure,oParams,con);
        }

        public static RecordSet ViewCamapaignReportSars(string ClientName)
        {
            var oParams = new DBParamCollection
            {
                {"@ClientName", ClientName }

            };
            return new RecordSet("[dbo].[ViewAllCampaignReportSARS]", QueryType.StoredProcedure, oParams);
        }
        public static List<SelectListItem> PopulateRoles()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["NicsDebtorSearch"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = " SELECT Id, Name FROM Roles";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = sdr["Name"].ToString(),
                                Value = sdr["Id"].ToString()
                            });
                        }
                    }

                    con.Close();
                }
            }

            return items;
        }
        public static List<SelectListItem> Clients()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["LexiirDWH"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = " SELECT DatabaseName FROM SourceList WHERE Enabled=1";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = sdr["DatabaseName"].ToString(),
                                Value = sdr["DatabaseName"].ToString()
                            });
                        }
                    }

                    con.Close();
                }
            }

            return items;
        }

        public static List<SelectListItem> SupervisorBooks()
        {
            var identityUserName = System.Web.HttpContext.Current.User.Identity.Name;
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["NicsDebtorSearch"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = " SELECT BookName FROM Books WHERE UserName = @UserName";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.Add(new SqlParameter("@UserName", identityUserName));
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = sdr["BookName"].ToString(),
                                Value = sdr["BookName"].ToString()
                            });
                        }
                    }

                    con.Close();
                }
            }

            return items;
        }
        public static List<SelectListItem> SecurityQuestions()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["NicsDebtorSearch"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = " SELECT Question FROM SecurityQuestions";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = sdr["Question"].ToString(),
                                Value = sdr["Question"].ToString()
                            });
                        }
                    }

                    con.Close();
                }
            }

            return items;
        }

        public static List<SelectListItem> ReportTypes()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["NicsDebtorSearch"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = " SELECT Id , Description FROM ReportsType";
                using (SqlCommand cmd = new SqlCommand(query))
                {


                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = sdr["Description"].ToString(),
                                Value = sdr["Id"].ToString()
                            });
                        }
                    }

                    con.Close();
                }
            }

            return items;
        }
        public static string GetNameByEmail()
        {
            var displayName = System.Web.HttpContext.Current.User.Identity.Name;

            var identityUserName = System.Web.HttpContext.Current.User.Identity.Name;
            string constr = ConfigurationManager.ConnectionStrings["NicsDebtorSearch"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {

                string query = "SELECT Name, Surname FROM Users where Email = @Email";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.Add(new SqlParameter("@Email", identityUserName));
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            var name = sdr["Name"].ToString();
                            var surname = sdr["Surname"].ToString();
                            displayName = name;

                        }
                    }

                    con.Close();
                }
            }

            return displayName;
        }

        public static User GetRoleByUserName() {

         
            var role = new User();
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
                {
                 role = userRepository.FindBy(u=>u.UserName == HttpContext.Current.User.Identity.Name).FirstOrDefault();
                }
            }
           
            return role;

        }

        public static User GetUserRoleByEmail()
        {

            var role = new User();
            var identityUserName = System.Web.HttpContext.Current.User.Identity.Name;
            string constr = ConfigurationManager.ConnectionStrings["NicsDebtorSearch"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                
                string query = "SELECT * FROM Users where Email = @Email";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.Add(new SqlParameter("@Email", identityUserName));
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            role.Name = sdr["Name"].ToString();
                            role.Surname = sdr["Surname"].ToString();
                            role.Department = sdr["Department"].ToString();
                           
                        }
                    }

                    con.Close();
                }
            }

            return role;
        }

        private static void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (userRepository != null)
                {
                    userRepository.Dispose();
                }
                
            }

        }

        public static IQueryable<DebtorSearchViewModel> GetDebtors(DebtorSearchViewModel searchModel)
        {
            
            //var debtor = Context.Debtors.AsQueryable();
            //var emails = Context.Emails.AsQueryable();
            //var address = Context.Addresses.AsQueryable();

            var query = (from debtor in Context.Debtors
                         join email in Context.Emails on debtor.AccountNumber equals email.AccountNumber into ps
                         join address in Context.Addresses on debtor.AccountNumber equals address.AccountNumber into js
                         from Email in ps.DefaultIfEmpty()
                         from Address in js.DefaultIfEmpty()
                         select new DebtorSearchViewModel {
                            ClientName = debtor.ClientName,
                            AccountNumber = debtor.AccountNumber,
                            Name = debtor.FirstName,
                            surname = debtor.Surname,
                            IdNumber = debtor.IdNumber,
                            ContactNo = debtor.ContactNumber,
                            EmailAddres = Email.Emails,
                            Address1 = Address.Address1,
                            Address2 = Address.Address2,
                            Code = Address.AddressCode,
                            ClientRef = debtor.ClientRef,
                            VatNo = debtor.Vat,
                            CompanyReg =debtor.CompanyReg
                        }).Distinct();

            if (!string.IsNullOrEmpty(searchModel.IdNumber))
                query = query.Where(x=>x.IdNumber.Contains(searchModel.IdNumber));

            if (!string.IsNullOrEmpty(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if (!string.IsNullOrEmpty(searchModel.ContactNo))
                query = query.Where(x => x.ContactNo.Contains(searchModel.ContactNo));

            if (!string.IsNullOrEmpty(searchModel.surname))
                query = query.Where(x => x.surname.Contains(searchModel.surname));

            if (!string.IsNullOrEmpty(searchModel.AccountNumber))
                query = query.Where(x => x.AccountNumber.Contains(searchModel.AccountNumber));

            if (!string.IsNullOrEmpty(searchModel.ClientRef))
                query = query.Where(x => x.ClientRef.Contains(searchModel.ClientRef));
            
            if (!string.IsNullOrEmpty(searchModel.EmailAddres))
                query = query.Where(x => x.EmailAddres.Contains(searchModel.EmailAddres));

            if (!string.IsNullOrEmpty(searchModel.VatNo))
                query = query.Where(x => x.VatNo.Contains(searchModel.VatNo));

            if (!string.IsNullOrEmpty(searchModel.CompanyReg))
                query = query.Where(x => x.CompanyReg.Contains(searchModel.CompanyReg));

            if (!string.IsNullOrEmpty(searchModel.Address1))
                query = query.Where(x => x.Address1.Contains(searchModel.Address1) || x.Address2.Contains(searchModel.Address1));
         
            if (!string.IsNullOrEmpty(searchModel.Address2))
                query = query.Where(x => x.Address1.Contains(searchModel.Address2) || x.Address2.Contains(searchModel.Address1));

            return query.Distinct();
        }
        public static List<SelectListItem> SourceList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["NicsDebtorSearch"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = " SELECT ConString , Name FROM ConnectionStrings";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = sdr["Name"].ToString(),
                                Value = sdr["ConString"].ToString()
                            });
                        }
                    }

                    con.Close();
                }
            }

            return items;
        }

    }

  
}