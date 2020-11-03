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
                var dönüt = Sifreleme.CalculateMd5Hash(GelenUye.Sifre);
                var toplu = ctx.Uyeler.ToList();
                var bulunanone = ctx.Uyeler.Where(u => u.Eposta == GelenUye.Eposta);
                if (bulunanone.Count()!=0)
                {
                    TempData["mesaj"] = "email is already in use";
                    TempData["durum"] = true;
                    return new RedirectResult("/Register/Index");
                }
                GelenUye.Sifre = dönüt;
                GelenUye.AktifMi = 0;
                string mail = GelenUye.Eposta;
                ctx.Uyeler.Add(GelenUye);
                ctx.SaveChanges();
                int i=GelenUye.Uyeid;
                //string url = string.Format("<a href=\"www.zaman.com/Register/UyeKayit/{0}\"></a>",i); //Link olarak dönmüyor.
                string link = string.Format("<a href=\"http://www.timemail.org/Register/UyeKayit/{0}\">Onay için buraya tıklayınız.</a>", i);
                SendMail.Onaymail(mail,link);
                return RedirectToAction("Onay");
            }
            
          
        }
        public IActionResult Onay()
        {
            return View();
        }
        public IActionResult UyeKayit(int? id) 
        {
            using (ZamanContext ctx = new ZamanContext())
            {

                var bulunan = ctx.Uyeler.Find(id);
                if (bulunan!=null)
                {
                    bulunan.AktifMi = 1;
                    ctx.SaveChanges();
                }
                return View();
            }
        }
    }
}
