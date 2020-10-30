using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gelecek.Models;
using Microsoft.AspNetCore.Mvc;


namespace Gelecek.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Uye GelenUye)
        {
            using (ZamanContext ctx=new ZamanContext())
            {
                var toplu = ctx.Uyeler.ToList();
                foreach (var item in toplu)
                {
                    if (item.Eposta == GelenUye.Eposta)
                    {
                        TempData["mesaj"] = "email is already in use";
                        TempData["durum"] = true;
                        return new RedirectResult("/Register/Index");
                    }
                }
                string mail = GelenUye.Eposta;
                 SendMail.Onaymail(mail);
                TempData["ad"] =GelenUye.Ad;
                TempData["soyad"] = GelenUye.Soyad;
                TempData["posta"] = GelenUye.Eposta;
                TempData["sifre"] = GelenUye.Sifre;
                return RedirectToAction("Onay");
            }
            
          
        }
        public IActionResult Onay()
        {
            return View();
        }
        public IActionResult UyeKayit() //maildeki linke tıklandığında Üye kaydı yapılacak. 
        {
            
            using (ZamanContext ctx = new ZamanContext())
            {
                Uye gelenUye = new Uye();
                gelenUye.Ad = TempData["ad"].ToString();
                gelenUye.Soyad = TempData["soyad"].ToString();
                gelenUye.Eposta = TempData["posta"].ToString();
                gelenUye.Sifre = TempData["sifre"].ToString();
                var dönüt = Sifreleme.CalculateMd5Hash(gelenUye.Sifre);
                gelenUye.Sifre = dönüt;
                ctx.Uyeler.Add(gelenUye);
                ctx.SaveChanges();
                return RedirectToAction("Index","SignIn");

            }
        }
    }
}
