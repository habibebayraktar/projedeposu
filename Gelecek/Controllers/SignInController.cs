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
                var SifreDönüt = Sifreleme.CalculateMd5Hash(Gelenbilgi.Sifre);
                var toplu = ctx.Uyeler.ToList();
                foreach (var item in toplu)
                {
                    if (item.Eposta==Gelenbilgi.Eposta)
                    {
                        if (item.Sifre==SifreDönüt)
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
                TempData["durum"] = true;
                TempData["mesaj"] = "Email or password is incorrect";
                return RedirectToAction("Index");
               
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
                        if (item.Eposta == değisiklik.Eposta)
                        {

                           if (item.AktifMi==0)
                           {
                              TempData["onay"] = true;
                              TempData["mesj"] = "Your membership is not active, you cannot change the password";
                              return RedirectToAction("Index");
                           }
                           else
                           {
                             int id = item.Uyeid;
                             string link = string.Format("<a href=\"www.zaman.com/Register/UyeKayit/{0}\"></a>", id); //Link olarak dönmüyor.
                             SendMail.newPasswordMail(değisiklik.Eposta, link);
                             TempData["id"] = id;
                            return RedirectToAction("Link");
                           }
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
                using (ZamanContext ctx = new ZamanContext())
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


