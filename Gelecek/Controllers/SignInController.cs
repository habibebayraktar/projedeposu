using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Gelecek.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gelecek.Controllers
{
    public class SignInController : Controller
    {
        public IActionResult Index()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Index(Uye Gelenbilgi)
        {
            using (ZamanContext ctx = new ZamanContext())
            {
                var SifreDönüt = Sifreleme.CalculateMd5Hash(Gelenbilgi.Sifre);
                var bulunanone = ctx.Uyeler.Where(u => u.Eposta == Gelenbilgi.Eposta && u.Sifre == SifreDönüt);
                if (bulunanone.Count() ==0)
                {
                    TempData["durum"] = true;
                    TempData["mesaj"] = "Email or password is incorrect";
                    return RedirectToAction("Index");
                }
                else
                {
                    var bulunan = ctx.Uyeler.FirstOrDefault(o => o.Eposta == Gelenbilgi.Eposta);

                    if (bulunan.AktifMi == 1)
                    {

                        var i = bulunan.Uyeid;
                        TempData["gidenid"] = i;
                        return RedirectToAction("Send", "SignIn", new { id = i });
                    }
                    else
                    {
                        TempData["durumtwo"] = true;
                        TempData["msj"] = "your membership is inactive";
                        return RedirectToAction("Index");

                    }
                }
               
            }
        }

           public IActionResult Send()
            {
               
                return View();

            }

            [HttpPost]
            public IActionResult Send(Posta GelenPosta)
            {

                using (ZamanContext ctx = new ZamanContext())
                {
                 var now = DateTime.Now;
                 if (GelenPosta.IletilecekZaman< now.AddDays(2))
                 {
                    //iletiler en az üç gün sonrasına iletilebilir.
                    TempData["drm"] = true;
                    TempData["mesaj"] = "E - mail can be sent at least 3 days later";
                 }
                 else
                 {
                    GelenPosta.NeZamanYazildi = now;
                    GelenPosta.UyeId = Convert.ToInt32(TempData["gidenid"]);
                    ctx.Postalar.Add(GelenPosta);
                    ctx.SaveChanges();
                    TempData["durum"] = true;
                    TempData["msj"] = "congratulations! sent to the future";
                    return RedirectToAction("Send");
                 }
                 return View();

                }
            }
            public IActionResult ForgotPassword()
            {

                return View();

            }
            [HttpPost]
            public IActionResult ForgotPassword(Uye değisiklik)
            {

                using (ZamanContext ctx = new ZamanContext())
                {
                    var hepsi = ctx.Uyeler.ToList();
                    var bulunanone = ctx.Uyeler.Where(u => u.Eposta == değisiklik.Eposta);

                if (bulunanone.Count() == 0)
                {
                    TempData["mesaj"] = "e - mail information is incorrect";
                    TempData["durum"] = true;
                    return RedirectToAction("ForgotPassword");
                }
                else
                {
                    var bul = ctx.Uyeler.FirstOrDefault(o=>o.Eposta==değisiklik.Eposta);
                    if (bul.AktifMi == 0)
                    {
                        TempData["onay"] = true;
                        TempData["mesj"] = "Your membership is not active, you cannot change the password";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        int id = bul.Uyeid;
                        string link = string.Format("<a href=\"http://www.timemail.org/SignIn/NewPassword/{0}\">Onay için buraya tıklayınız.</a>", id);
                        SendMail.newPasswordMail(değisiklik.Eposta, link);
                        TempData["id"] = id;
                        return RedirectToAction("Link");
                    }
                }

                }
                   
            }

            public IActionResult Link()
            {

                return View();

            }

            public IActionResult NewPassword()
            {
                return View();
            }

            [HttpPost]
            public IActionResult NewPassword(Uye yeniSifre) //şifre yenileme mailinde ,maildeki adrese tıkladığında burası işleme giricek
            {
                var dönüt = Sifreleme.CalculateMd5Hash(yeniSifre.Sifre);
                using (ZamanContext ctx = new ZamanContext())
                {

                    var i = Convert.ToInt32(TempData["id"]);
                    var b = ctx.Uyeler.Find(i);
                    b.Sifre = dönüt;
                    ctx.SaveChanges();
                    TempData["d"] = true;
                    TempData["m"] = "Password change is successful";
                    return RedirectToAction("Index");
                }
            }
        public IActionResult MailSend()
        {
            using (ZamanContext ctx=new ZamanContext())
            {
                var n = DateTime.Today;
                var PostalarTbl = ctx.Postalar.ToList();
                var bulunanone = ctx.Postalar.Where(p => p.IletilecekZaman == n);
                foreach (var item in bulunanone)
                {
                    if (item.IletilecekZaman == n)
                    {
                        SmtpClient client = new SmtpClient();
                        MailMessage message = new MailMessage();

                        client.Credentials = new NetworkCredential("time--@outlook.com", "habibe123");//kendi mailim ve şifrem.
                        client.Port = 587; //simple transfer protocol
                        client.Host = "smtp-mail.outlook.com";
                        client.EnableSsl = true;
                        message.From = new MailAddress("time--@outlook.com", "TiMe");
                        message.To.Add(item.PostaAdresi); //hangi mail'e yollanacak
                        message.Subject = item.MetinKonusu;
                        message.IsBodyHtml = true;
                        message.Body = item.Metin  + $"(posted on {item.NeZamanYazildi}) ";
                        client.Send(message);
                        ctx.Remove(ctx.Postalar.First(p => p.PostaId == item.PostaId));
                        ctx.SaveChanges();
                    }
                }
            }

           return View();
            }
        }
    }


