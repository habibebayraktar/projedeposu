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
            
            using (ZamanContext ctx=new ZamanContext())
            {
                var SifreDönüt = Sifreleme.CalculateMd5Hash(Gelenbilgi.Sifre);
                var uyeler = ctx.Uyeler.ToList();
                foreach (var item in uyeler)
                {
                    var Dbdönüt = Sifreleme.CalculateMd5Hash(item.Sifre);
                    if (item.Eposta == Gelenbilgi.Eposta && Dbdönüt == SifreDönüt)
                    {
                        var u = ctx.Uyeler.Find(Gelenbilgi.Eposta);
                        var id = u.Uyeid;
                        return RedirectToAction("Send","SignIn","id");
                        //**
                        //return RedirectToAction("Send","SignIn"); denedim ama Send viewına yönlendirme yapamıyorum.
                    }

                }
                return View();
            }
         
        }
        public IActionResult Send() //e posta gönderim sayfası
        {
            
            return View();

        }
        [HttpPost]
        public IActionResult Send(Posta GelenPosta) //e posta gönderim sayfası
        {

            using (ZamanContext ctx = new ZamanContext())
            {
                var now = DateTime.Now;
                GelenPosta.NeZamanYazildi = now;
                ctx.Postalar.Add(GelenPosta);
                ctx.SaveChanges();

                return View();
            }
        }
    }
}
