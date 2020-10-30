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
        
        public static bool Onaymail(string mailadresi)
        {
            SmtpClient client = new SmtpClient();
            MailMessage message = new MailMessage();

            string yol = "https://mertmtn.blogspot.com/2018/02/c-ile-aktivasyon-maili-gonderme.html";
            client.Credentials = new NetworkCredential("time--@outlook.com", "habibe123");//kendi mailim ve şifrem.
            client.Port = 587; //simple transfer protocol
            client.Host = "smtp.live.com"; //smtp.gmail.com
            client.EnableSsl = true;
            message.From = new MailAddress("time--@outlook.com", "TiMe");//kendi mail adresim gelicek
            message.To.Add(mailadresi); //hangi mail'e yollanacak
            message.Subject = "Confirmation code";
            message.Body = "Confirmation code" + "Confirmation Code To confirm your membership, you can perform your activation process from this address.\n\n" + yol;
            client.Send(message);
            return true;


        }
        public static bool newPasswordMail(string mailadresi)
        {
            SmtpClient client = new SmtpClient();
            MailMessage message = new MailMessage();

            Random rnd = new Random();

            string yol = "https://mertmtn.blogspot.com/2018/02/c-ile-aktivasyon-maili-gonderme.html";
            client.Credentials = new NetworkCredential("time--@outlook.com", "habibe123");//kendi mailim ve şifrem.
            client.Port = 587; //simple transfer protocol
            client.Host = "smtp.live.com"; //smtp.gmail.com
            client.EnableSsl = true;
            message.From = new MailAddress("time--@outlook.com", "TiMe");//kendi mail adresim gelicek
            message.To.Add(mailadresi); //hangi mail'e yollanacak
            message.Subject = "New Password ";
            message.Body = "New password Code" + "Hello! Please follow this link to log in to your TiMe account:\n\n" + yol;
            client.Send(message);
            return true;


        }
    }
}
