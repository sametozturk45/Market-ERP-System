using MarketKasaSistemi.DataAccess;
using MarketKasaSistemi.Entities;
using MarketKasaSistemi.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketKasaSistemi.Web.Areas.Yonetim.Controllers
{
    public class KullanicilarController : Controller
    {
        [GirisKontrol]
        public ActionResult Index()
        {
            using (UnitOfWork uow = new UnitOfWork())
                return View(uow.KullaniciRepository.ToList());
        }

        [GirisKontrol]
        public ActionResult Duzenle(int? id)
        {
            if (id != null)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Kullanici kullanici = uow.KullaniciRepository.GetItem(id);
                    if (kullanici != null)
                        return View(kullanici);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, GirisKontrol]
        public ActionResult Duzenle(int? id, Kullanici kullanici)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Kullanici updateUser = uow.KullaniciRepository.GetItem(kullanici.Id);
                Kullanici aktif = HttpContext.Session["User"] as Kullanici;
                updateUser.KullaniciAd = kullanici.KullaniciAd;
                updateUser.KullaniciSifre = kullanici.KullaniciSifre;
                if (kullanici != null)
                {
                    if (ModelState.IsValid)
                    {
                        uow.KullaniciRepository.Update(updateUser);
                        if(kullanici.Id == aktif.Id)
                            Session["User"] = null;
                        return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError("", "Bir Hata Oluştu");
                return View(updateUser);
            }
        }
        [GirisKontrol]
        public ActionResult Sil(int? id)
        {
            Kullanici aktif = HttpContext.Session["User"] as Kullanici;
            if (id != null && id != aktif.Id)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Kullanici kullanici = uow.KullaniciRepository.GetItem(id);
                    if (kullanici != null)
                        return View(kullanici);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost, ValidateAntiForgeryToken, ActionName("Sil"), GirisKontrol]
        public ActionResult SilOnay(int? id)
        {
            if (id != null)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Kullanici kullanici = uow.KullaniciRepository.GetItem(id);
                    if (kullanici != null)
                    {
                        uow.KullaniciRepository.Remove(kullanici);
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}