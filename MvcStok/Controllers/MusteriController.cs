using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;               // Sayfalama kullanabilmek için PagedList ekledik
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        DbMvcStokEntities db = new DbMvcStokEntities();

        [Authorize]
        public ActionResult Index(int sayfa = 1)
        {
            // müşteri Sayfasına sayfalama ekledik her sayfada 3 kişi olacak, durumu true olanları şekilde listeledik

            var musterilistesi = db.TblMusteri.Where(x => x.durum == true).ToList().ToPagedList(sayfa, 5);
            return View(musterilistesi);
        }


        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMusteri(TblMusteri p)    //Müşteri Ekleme İşlemi
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            else
            {
                p.durum = true;
                db.TblMusteri.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult MusteriSil(TblMusteri p)
        {
            var Musteribul = db.TblMusteri.Find(p.id);
            Musteribul.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(int id)         //müşterileri Guncelleme sayfasına taşıma
        {

            var musteri = db.TblMusteri.Find(id);

            return View("MusteriGetir", musteri);
        }

        public ActionResult MusteriGuncelle(TblMusteri p)          //Müşteri Güncelleme   İşlemi
        {
            var mus = db.TblMusteri.Find(p.id);
            mus.ad = p.ad;
            mus.Soyad = p.Soyad;
            mus.sehir = p.sehir;
            mus.bakiye = p.bakiye;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}