using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace  NICS.System.Security.Utils
{
   public class AdUser
   {
       private static readonly string DomainName = ConfigurationManager.AppSettings["domain"];
        public static string GetUser()
        {
           
            var userName = "";
            using (var context = new PrincipalContext(ContextType.Domain, DomainName))
            {
                using (var user = UserPrincipal.FindByIdentity(context, Environment.UserName))
                {
                    userName = user.DisplayName;

                }
            }
            return userName;

        }
        public static string GetEmail()
        {
            var Email = "";
            using (var context = new PrincipalContext(ContextType.Domain))
            {
                using (var user = UserPrincipal.FindByIdentity(context, Environment.UserName))
                {


                    Email = user.EmailAddress;

                }
            }
            return Email;

        }
        public static string GetSid()
        {
            var Sid = "";
            using (var context = new PrincipalContext(ContextType.Domain))
            {
                using (var user = UserPrincipal.FindByIdentity(context, Environment.UserName)) // HttpContext.Current.User.Identity.Name
                {
                    Sid = user.SamAccountName;
                }
            }
            return Sid;

        }

       public static List<string> GetAllUsers()
       {
           try
           {
               List<string> users = new List<string>();

               PrincipalContext context = new PrincipalContext(ContextType.Domain, DomainName);
               GroupPrincipal gPrincipal = GroupPrincipal.FindByIdentity(context, IdentityType.SamAccountName, "Domain Users");

                if (gPrincipal != null)
                {
                    foreach (Principal p in gPrincipal.GetMembers(false))
                    {

                        users.Add(p.DisplayName);
                    }
                }
              
                
                return users;
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }

        public static List<string> GetAllEmails()
        {
            try
            {
                List<string> users = new List<string>();

                PrincipalContext context = new PrincipalContext(ContextType.Domain, DomainName);
              
                using (context)
                {
                    using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                    {
                        foreach (var result in searcher.FindAll())
                        {
                            
                            DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;
                            if (de.Properties["mail"].Value != null)
                            {
                                users.Add(de.Properties["mail"].Value.ToString());
                            }
                        }
                    }
                }

                return users;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

