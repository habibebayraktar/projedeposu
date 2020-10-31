using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
namespace Gelecek.Models
{
    public class SendMail
    {
        
        public static bool Onaymail(string mailadresi,string link)
        {
            SmtpClient client = new SmtpClient();
            MailMessage message = new MailMessage();

            client.Credentials = new NetworkCredential("time--@outlook.com","habibe123");//kendi mailim ve şifrem.
            client.Port = 587; //simple transfer protocol
            client.Host = "smtp-mail.outlook.com"; //smtp.gmail.com
            client.EnableSsl = true;
            message.From = new MailAddress("time--@outlook.com","TiMe");//kendi mail adresim gelicek
            message.To.Add(mailadresi); //hangi mail'e yollanacak
            message.Subject = "Confirmation code";
            message.IsBodyHtml = true;
            message.Body ="Confirmation Code To confirm your membership, you can perform your activation process from this address.\n\n" +link;
          
            client.Send(message);
            return true;


        }
        public static bool newPasswordMail(string mailadresi, string link)
        {
            SmtpClient client = new SmtpClient();
            MailMessage message = new MailMessage();

            
            client.Credentials = new NetworkCredential("time--@outlook.com", "habibe123");//kendi mailim ve şifrem.
            client.Port = 587; //simple transfer protocol
            client.Host = "smtp.live.com"; //smtp.gmail.com
            client.EnableSsl = true;
            message.From = new MailAddress("time--@outlook.com", "TiMe");//kendi mail adresim gelicek
            message.To.Add(mailadresi); //hangi mail'e yollanacak
            message.Subject = "New Password ";
            message.IsBodyHtml = true;
            message.Body = "New password Code" + "Hello! Please follow this link to log in to your TiMe account:\n\n" + link;
            client.Send(message);
            return true;

        }
    }
}
