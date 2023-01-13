using DebtorSearch.Business_Objects;
using DebtorSearch.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DebtorSearch
{
    public class MvcApplication : System.Web.HttpApplication
    {
        readonly SystemErrosRepository _errors = new SystemErrosRepository();
       
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var error = Server.GetLastError();
            if (error != null)
            {
                if (error.Message != null)
                {
                    var user = "";
                    if (HttpContext.Current.Request.LogonUserIdentity != null)
                    {
                        user = HttpContext.Current.Request.LogonUserIdentity.Name;
                    }

                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        SystemErrors errors = new SystemErrors
                        {
                            SessionId = Session.SessionID,
                            ErrorMessage = error.Message,
                            ErrorStackTrace = error.StackTrace,
                            ErrorSource = error.Source,
                            SystemUser = HttpContext.Current.User.Identity.Name
                        };
                        _errors.Add(errors);
                        _errors.Save();
                    }
                    else
                    {
                        SystemErrors errors = new SystemErrors
                        {
                            SessionId = Session.SessionID,
                            ErrorMessage = error.Message,
                            ErrorStackTrace = error.StackTrace,
                            ErrorSource = error.Source,
                            SystemUser = user
                        };
                        _errors.Add(errors);
                        _errors.Save();
                    }
                    
                    HttpContext.Current.Response.Redirect("~/Shared/Error");

                }
            }
        }

    }
}
