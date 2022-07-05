using MarketKasaSistemi.DataAccess;
using MarketKasaSistemi.Entities;
using MarketKasaSistemi.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketKasaSistemi.Web.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            if (Session["User"] == null)
                return View();
            else
                return RedirectToAction("Satis", "Home");
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Login(Kullanici kullanici)
        {
            if (kullanici != null)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    if (uow.KullaniciRepository.Login(kullanici.KullaniciAd,kullanici.KullaniciSifre))
                    {
                        Kullanici giris = uow.KullaniciRepository.ToList().FirstOrDefault(x => x.KullaniciAd == kullanici.KullaniciAd);
                        Session.Add("User", giris);
                        return RedirectToAction("Satis", "Home");
                    }
                }
            }

            ModelState.AddModelError("","Hatalı Giriş");
            return View(kullanici);
        }

        [GirisKontrol]
        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Login","Login");
        }

        public ActionResult Register()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                List<Kullanici> kullaniciList = uow.KullaniciRepository.ToList();
                List<Personel> personelList = uow.PersonelRepository.ToList();
                List<Personel> unRegistered = new List<Personel>();
                foreach (var item in personelList)
                {
                   if(kullaniciList.FirstOrDefault(x=>x.Personel.Id == item.Id) == null)
                        unRegistered.Add(item);
                }

                ViewBag.Personeller = new SelectList(unRegistered, "Id", "PersonelAd");
            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Register(Kullanici kullanici)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                if (uow.KullaniciRepository.ToList().FirstOrDefault(x => x.Personel.Id == kullanici.Personel.Id) == null)
                {
                    if (uow.KullaniciRepository.ToList().FirstOrDefault(x => x.KullaniciAd == kullanici.KullaniciAd) == null)
                        if (ModelState.IsValid)
                        {
                            kullanici.Personel = uow.PersonelRepository.GetItem(kullanici.Personel.Id);
                            uow.KullaniciRepository.Add(kullanici);
                            return RedirectToAction("Login","Login");
                        }
                }
                ModelState.AddModelError("", "Bu kullanıcı adı daha önceden alınmış gibi görünüyor.");
                List<Kullanici> kullaniciList = uow.KullaniciRepository.ToList();
                List<Personel> personelList = uow.PersonelRepository.ToList();
                List<Personel> unRegistered = new List<Personel>();
                foreach (var item in personelList)
                {
                    if (kullaniciList.FirstOrDefault(x => x.Personel.Id == item.Id) == null)
                        unRegistered.Add(item);
                }

                ViewBag.Personeller = new SelectList(unRegistered, "Id", "PersonelAd");
                return View(kullanici);
            }
        }
    }
}