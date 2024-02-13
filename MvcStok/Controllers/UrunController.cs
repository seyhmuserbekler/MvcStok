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
    public class UrunController : Controller
    {
        // GET: Urun
        DbMvcStokEntities db=new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index(string p,int sayfa=1)    //Ürünleri durumu true olanları listeleme
        {
            var urunler = db.TblUrunler.Where(x => x.durum == true);

          if (!string.IsNullOrEmpty(p))
            {
                urunler = urunler.Where(x => x.ad.Contains(p) && x.durum == true);
            }
            return View(urunler.ToList().ToPagedList(sayfa ,5));
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> ktgr = (from x in db.TblKategori.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.ad,
                                             Value = x.id.ToString(),
                                         }).ToList();
            ViewBag.drop = ktgr;
                                        
            return View(); 
            
            }

        [HttpPost]
        public ActionResult YeniUrun(TblUrunler p)
        {
            var ktgr = db.TblKategori.Where(x=>x.id==p.TblKategori.id).FirstOrDefault();
            p.TblKategori = ktgr;
            db.TblUrunler.Add(p);
            p.durum = true;
            db.SaveChanges();
            return RedirectToAction("Index");   

        }
        public ActionResult UrunGetir(int id)         //Urunleri Güncelleme Sayfasına Taşıma İşlemi
        {
            List<SelectListItem> ktgr = (from x in db.TblKategori.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.ad,
                                             Value = x.id.ToString(),
                                         }).ToList();
            
            var urn = db.TblUrunler.Find(id);
             ViewBag.drop = ktgr;
            return View("UrunGetir", urn);
        }

        public ActionResult UrunGuncelle(TblUrunler p)          //Urun Güncelleme   İşlemi
        {
            var urun = db.TblUrunler.Find(p.id);
            urun.ad = p.ad;
            urun.marka = p.marka;
            urun.stok = p.stok;
            urun.alisfiyat = p.alisfiyat;
            urun.satisfiyat = p.satisfiyat;
            var ktgr = db.TblKategori.Where(x => x.id == p.TblKategori.id).FirstOrDefault();
            urun.kategori = ktgr.id;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(TblUrunler p)
        {
            var urunbul = db.TblUrunler.Find(p.id);
            urunbul.durum = false;
            db.SaveChanges();   
            return RedirectToAction("Index");
    }
    }

   
}