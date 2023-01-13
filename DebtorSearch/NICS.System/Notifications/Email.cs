using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NICS.System.Notifications
{
  public static class Email
    {
        public static void Sendmail(MailMessage message) {
        
            var client = new SmtpClient()
            {
                Port =587,
                Host = "smtp.gmail.com",
                Credentials = new NetworkCredential("itdev@nics.co.za", "itd001ni"),
                EnableSsl = true,
                
            };

           client.Send(message);
           
        }
       
    }

   
}
