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
        public IActionResult Index() //görevi view'ı bize getirmek.
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Uye GelenUye) //Uyeyi veri tabanına ekleyecek
        {

            var dönüt= Sifreleme.CalculateMd5Hash(GelenUye.Sifre);
            GelenUye.Sifre = dönüt;
            using (ZamanContext ctx=new ZamanContext())
            {
                
                var toplu = ctx.Uyeler.ToList();
                foreach (var item in toplu)
                {
                    if (item.Eposta==GelenUye.Eposta) //db de, girilen eposta kayıtlıysa kayıt işlemini gerçekleştirmiyor.Fakat bunun bilgisini kullanıcıya yanısatamadım ????
                    {
                        return new RedirectResult("/Register/Index");
                    }
                }
                ctx.Uyeler.Add(GelenUye);
                ctx.SaveChanges();
                return RedirectToAction("Onay");
                
            }
          
        }
        public IActionResult Onay()
        {
            //REGİSTER butonuna tıkladığında e-mail onay sayfasına yönlendiremiyorum.
            //e postaa onay sayfasına yönlendirilsem e posta onay kodlarını yazamıyorum.
            return View();
        }
    }
}
