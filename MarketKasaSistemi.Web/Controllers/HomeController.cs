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
    
    public class HomeController : Controller
    {
        [GirisKontrol]
        public ActionResult Barkod()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken, GirisKontrol]
        public ActionResult Barkod(Urun urun)
        {
            using (UnitOfWork uow = new UnitOfWork())
                return View(uow.UrunRepository.GetItem(urun.Id));
        }
        [GirisKontrol]
        public ActionResult Satis()
        {
            List<Satis> satislar = Session["satislar"] as List<Satis>;
            if (satislar == null)
            {
                satislar = new List<Satis>();
                Session["satislar"] = new List<Satis>();
            }
                return View(satislar);
        }
        [GirisKontrol]
        public ActionResult SatisEkle(int? id)
        {
            Satis newSatis = new Satis();
            newSatis.Urun = new Urun();
            if(id != null)
            {
                var satislar = Session["satislar"] as List<Satis>;
                newSatis = satislar.Find(x => x.Id == id);
            }
            return View(newSatis);
        }
        [HttpPost, ValidateAntiForgeryToken, GirisKontrol]
        public ActionResult SatisEkle(Satis satis, int? id)
        {
            if (ModelState.IsValid)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Urun yeniUrun = uow.UrunRepository.GetItem(satis.UrunBarkod);

                    List<Satis> satislar = Session["satislar"] as List<Satis>;

                    if (satislar.Count > 0 && id != null)
                    {
                        int idx = satislar.FindIndex(x => x.Id == satis.Id);
                        satis.Urun = yeniUrun;
                        (Session["satislar"] as List<Satis>)[idx] = satis;
                        return RedirectToAction("Satis");
                    }
                    if (yeniUrun != null && satis.SatisAdet < yeniUrun.UrunStokAdet)
                    {
                        satis.Id = (Session["satislar"] as List<Satis>).Count + 1;
                        satis.Urun = yeniUrun;
                        (Session["satislar"] as List<Satis>).Add(satis);
                        return RedirectToAction("Satis");
                    }
                }

            }
            return View(satis);
        }
        [GirisKontrol]
        public ActionResult SatisSil(int? id)
        {
            List<Satis> satislar = Session["satislar"] as List<Satis>;
            if (satislar != null)
            {
                Satis satis = satislar.Find (x => x.Id == id);
                (Session["satislar"] as List<Satis>).Remove(satis);
            }
            return RedirectToAction("Satis");
        }

        [GirisKontrol]
        public ActionResult SatisTamamla(OdemeTip odemeTip)
        {
            List<Satis> satislar = Session["satislar"] as List<Satis>;
            if (ModelState.IsValid)
            {
                Fis newFis;
                odemeTip = new OdemeTip { Id = 1, OdemeTipAd = "Nakit" };
                using (UnitOfWork uow = new UnitOfWork())
                {
                    newFis = new Fis
                    {
                        FisTarih = DateTime.Now,
                        Personel = uow.PersonelRepository.GetItem((Session["User"] as Kullanici).Personel.Id),
                        OdemeTip = odemeTip
                    };

                    newFis.Id = Convert.ToInt32(uow.FisRepository.Add(newFis));

                    foreach (var item in satislar)
                    {
                        item.Fis = newFis;
                        uow.SatisRepository.Add(item);
                    }
                }
                Session["satislar"] = null;
                return RedirectToAction("Satis");
            }
            return View(satislar);
        }

        
        [GirisKontrol]
        public ActionResult IadeEt()
        {
            Session["Satis"] = null;
                return View();
        }
        [HttpPost, ValidateAntiForgeryToken, GirisKontrol]
        public ActionResult IadeEt(int fisno)
        {
            Session["Satis"] = null;
            using (UnitOfWork uow = new UnitOfWork())
                return View(uow.SatisRepository.ToList().FindAll(x=>x.Fis.Id == fisno));
        }

        [GirisKontrol]
        public ActionResult IadeUrunGuncelle(int? id)
        {
            
            return View();
        }

        [GirisKontrol]
        public ActionResult IadeUrunSil(int? id)
        {
            // satis cek (by id)
            return View();
        }

        [GirisKontrol]
        public ActionResult ZRaporu()
        {
            Session["Satis"] = null;
            using (UnitOfWork uow = new UnitOfWork())
            return View(uow.SatisRepository.ToList().FindAll(x => x.Fis.FisTarih.Day == DateTime.Now.Day));
        }
    }
}