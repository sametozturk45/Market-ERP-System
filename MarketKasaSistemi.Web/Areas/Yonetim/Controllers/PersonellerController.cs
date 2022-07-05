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
    public class PersonellerController : Controller
    {
        [GirisKontrol]
        public ActionResult Index()
        {
            using (UnitOfWork uow = new UnitOfWork())
                return View(uow.PersonelRepository.ToList());
        }
        [GirisKontrol]
        public ActionResult Ekle()
        {
            using (UnitOfWork uow = new UnitOfWork())
                ViewBag.PersonelTip = new SelectList(uow.PersonelTipRepository.ToList(),"Id", "PersonelTipAd");
                return View();
        }
        [HttpPost, ValidateAntiForgeryToken, GirisKontrol]
        public ActionResult Ekle(Personel personel)
        {
            if (ModelState.IsValid)
            {
                personel.PersonelBaslangicTarih = DateTime.Now;
                using (UnitOfWork uow = new UnitOfWork())
                {
                    uow.PersonelRepository.Add(personel);
                    return RedirectToAction("Index");
                }
            }
            return View(personel);
        }
        [GirisKontrol]
        public ActionResult Duzenle(int? id)
        {
            if (id != null)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Personel personel = uow.PersonelRepository.GetItem(id);
                    ViewBag.PersonelTip = new SelectList(uow.PersonelTipRepository.ToList(),"Id", "PersonelTipAd");
                    if (personel != null)
                        return View(personel);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, GirisKontrol]
        public ActionResult Duzenle(int? id, Personel personel)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Kullanici aktif = HttpContext.Session["User"] as Kullanici;
                Personel updatePersonel = uow.PersonelRepository.GetItem(personel.Id);
                updatePersonel.PersonelAd = personel.PersonelAd;
                updatePersonel.PersonelSoyad = personel.PersonelSoyad;
                updatePersonel.PersonelTip = personel.PersonelTip;
                if (personel != null)
                {
                    if (ModelState.IsValid)
                    {
                        uow.PersonelRepository.Update(updatePersonel);
                        if (updatePersonel.Id == aktif.Personel.Id)
                            Session["User"] = null;
                        return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError("", "Bir Hata Oluştu");
                return View(updatePersonel);
            }
        }
        [GirisKontrol]
        public ActionResult Sil(int? id)
        {
            if (id != null)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Personel personel = uow.PersonelRepository.GetItem(id);
                    if (personel != null)
                        return View(personel);
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
                    Kullanici aktif = HttpContext.Session["User"] as Kullanici;
                    Personel personel = uow.PersonelRepository.GetItem(id);
                    List<Kullanici> kullanicilar = uow.KullaniciRepository.ToList();
                        if (kullanicilar.FirstOrDefault(x => x.Personel.Id == personel.Id) != null)
                            uow.KullaniciRepository.Remove(kullanicilar.FirstOrDefault(x => x.Personel.Id == personel.Id));
                    if (personel != null)
                    {
                        uow.PersonelRepository.Remove(personel);
                        if (personel.Id == aktif.Personel.Id)
                            Session["User"] = null;
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}