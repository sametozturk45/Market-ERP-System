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
    public class UrunlerController : Controller
    {
        [GirisKontrol]
        public ActionResult Index()
        {
            using (UnitOfWork uow = new UnitOfWork())
                return View(uow.UrunRepository.ToList());
        }
        [GirisKontrol]
        public ActionResult Ekle()
        {
            using (UnitOfWork uow = new UnitOfWork()) {
                ViewBag.Kategoriler = new SelectList(uow.KategoriRepository.ToList(), "Id", "KategoriAd");
                ViewBag.Vergiler = new SelectList(uow.VergiRepository.ToList(),"Id","VergiMiktar");
            }
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken, GirisKontrol]
        public ActionResult Ekle(Urun urun)
        {
            if (ModelState.IsValid)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    uow.UrunRepository.Add(urun);
                    return RedirectToAction("Index");
                }
            }
            return View(urun);
        }
        [GirisKontrol]
        public ActionResult Duzenle(int? id)
        {
            if (id != null)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Urun urun = uow.UrunRepository.GetItem(id);
                    ViewBag.Kategoriler = new SelectList(uow.KategoriRepository.ToList(), "Id", "KategoriAd");
                    ViewBag.Vergiler = new SelectList(uow.VergiRepository.ToList(), "Id", "VergiMiktar");
                    if (urun != null)
                        return View(urun);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, GirisKontrol]
        public ActionResult Duzenle(int? id, Urun urun)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Urun updateUrun = uow.UrunRepository.GetItem(urun.Id);
                updateUrun.UrunAd = urun.UrunAd;
                updateUrun.UrunFiyat = urun.UrunFiyat;
                updateUrun.UrunStokAdet = urun.UrunStokAdet;
                updateUrun.Kategori.Id = urun.Kategori.Id;
                updateUrun.Vergi.Id = urun.Vergi.Id;
                if (urun != null)
                {
                    if (ModelState.IsValid)
                    {
                        uow.UrunRepository.Update(updateUrun);
                        return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError("", "Bir Hata Oluştu");
                return View(updateUrun);
            }
        }
        [GirisKontrol]
        public ActionResult Sil(int? id)
        {
            if (id != null)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Urun urun = uow.UrunRepository.GetItem(id);
                    if (urun != null)
                        return View(urun);
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
                    Urun urun = uow.UrunRepository.GetItem(id);
                    if (urun != null)
                    {
                        uow.UrunRepository.Remove(urun);
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}