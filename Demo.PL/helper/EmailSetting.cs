using Demo.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Demo.PL.helper
{
    public class EmailSetting
    {
        public static void SendEmail( Email email)
        {
            var Cliant = new SmtpClient("smtp.gmail.com", 587);
            
          Cliant.EnableSsl = true;
            Cliant.Credentials = new NetworkCredential("omar01reda@gmail.com", "epjsegdskepdccfe");
            Cliant.Send("omar01reda@gmail.com", email.To, email.Subject, email.Body);  

        }
    }
}
