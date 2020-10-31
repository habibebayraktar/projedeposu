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
                foreach (var item in toplu)
                {
                    if (item.Eposta == GelenUye.Eposta)
                    {
                        TempData["mesaj"] = "email is already in use";
                        TempData["durum"] = true;
                        return new RedirectResult("/Register/Index");
                    }
                }
                GelenUye.Sifre = dönüt;
                GelenUye.AktifMi = 0;
                string mail = GelenUye.Eposta;
                ctx.Uyeler.Add(GelenUye);
                ctx.SaveChanges();
                int i=GelenUye.Uyeid;
                string url = string.Format("<a href=\"www.zaman.com/Register/UyeKayit/{0}\"></a>",i); //Link olarak dönmüyor.
                SendMail.Onaymail(mail,url);
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
                var sonuc = ctx.Uyeler.Where(u => u.Uyeid == bulunan.Uyeid);
                if (sonuc!=null)
                {
                    bulunan.AktifMi = 1;
                    ctx.Uyeler.Add(bulunan);
                    ctx.SaveChanges();

                    return RedirectToAction("Index", "SignIn");
                }
                return View();
               
                

            }
        }
    }
}
