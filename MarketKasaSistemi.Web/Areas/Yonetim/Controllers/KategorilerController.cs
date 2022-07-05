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
    public class KategorilerController : Controller
    {
        [GirisKontrol]
        public ActionResult Index()
        {
            using (UnitOfWork uow = new UnitOfWork())
                return View(uow.KategoriRepository.ToList());
        }
        [GirisKontrol]
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken,GirisKontrol]
        public ActionResult Ekle(Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    uow.KategoriRepository.Add(kategori);
                    return RedirectToAction("Index");
                }
            }
            return View(kategori);
        }

        [GirisKontrol]
        public ActionResult Duzenle(int? id)
        {
            if (id != null)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Kategori kategori = uow.KategoriRepository.GetItem(id);
                    if (kategori != null)
                        return View(kategori);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken,GirisKontrol]
        public ActionResult Duzenle(int? id, Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    uow.KategoriRepository.Update(kategori);
                    return RedirectToAction("Index");
                }
            }
            return View(kategori);
        }
        [GirisKontrol]
        public ActionResult Sil(int? id)
        {
            if (id != null)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Kategori kategori = uow.KategoriRepository.GetItem(id);
                    if (kategori != null)
                        return View(kategori);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost, ValidateAntiForgeryToken, ActionName("Sil"),GirisKontrol]
        public ActionResult SilOnay(int? id)
        {
            if (id != null)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Kategori kategori = uow.KategoriRepository.GetItem(id);
                    if (kategori != null)
                    {
                        uow.KategoriRepository.Remove(kategori);
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}