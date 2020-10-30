using System;
using System.Collections.Generic;
using System.Linq;
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
                //SORUN VAR EĞER BİLGİLER YANLIŞ İSE DE GİRİŞ YAPIYOR ??DÖNÜP BAK
                var SifreDönüt = Sifreleme.CalculateMd5Hash(Gelenbilgi.Sifre);
                var loggedinuser = ctx.Uyeler.Where(u => u.Eposta == Gelenbilgi.Eposta && u.Sifre == SifreDönüt);
                if (loggedinuser == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var bulunan = ctx.Uyeler.FirstOrDefault(o => o.Eposta == Gelenbilgi.Eposta);
                    var i = bulunan.Uyeid;
                    TempData["gidenid"] = bulunan.Uyeid;
                    return RedirectToAction("Send", "SignIn", new { id = i });
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
                GelenPosta.NeZamanYazildi = now;
                GelenPosta.UyeId = Convert.ToInt32(TempData["gidenid"]);
                ctx.Postalar.Add(GelenPosta);
                ctx.SaveChanges();
                return RedirectToAction("Send");
               
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
                foreach (var item in hepsi)
                {
                    if (item.Eposta==değisiklik.Eposta) 
                    {
                        SendMail.newPasswordMail(değisiklik.Eposta);
                        TempData["id"] = item.Uyeid;
                        
                        
                        return RedirectToAction("Link");
                    }
                }
                TempData["mesaj"] = "e-posta bilgisi yanlis";
                TempData["durum"] = true;
                return RedirectToAction("ForgotPassword");

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
            using (ZamanContext ctx=new ZamanContext())
            {
                
             
                var i = Convert.ToInt32(TempData["id"]);
                var b = ctx.Uyeler.Find(i);
                b.Sifre = dönüt;
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }


}
