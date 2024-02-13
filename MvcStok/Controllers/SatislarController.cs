using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;               // Sayfalama kullanabilmek için PagedList ekledik
using PagedList.Mvc;
namespace MvcStok.Controllers
{
    public class SatislarController : Controller
    {
        // GET: Satislar
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index(int sayfa=1)
        {
            var satislistele=db.TblSatislar.ToList().ToPagedList(sayfa,5);    
            return View(satislistele);
        }


        [HttpGet]
        public ActionResult YeniSatis() 
        {
            //Ürünler
            List<SelectListItem> urun = (from x in db.TblUrunler.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.ad,
                                             Value = x.id.ToString(),
                                         }).ToList();
            ViewBag.drop1 = urun;

            //Personel
            List<SelectListItem> pers = (from x in db.TblPersonel.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.ad+" "+ x.soyad,
                                             Value = x.id.ToString(),
                                         }).ToList();
            ViewBag.drop2 = pers;

            // Müşteriler 
            List<SelectListItem> musteri = (from x in db.TblMusteri.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.ad +" " + x.Soyad,
                                             Value = x.id.ToString(),
                                         }).ToList();
            ViewBag.drop3 = musteri;
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(TblSatislar p)
        {
            var urun = db.TblUrunler.Where(x => x.id == p.TblUrunler.id).FirstOrDefault();
            var musteri = db.TblMusteri.Where(x => x.id == p.TblMusteri.id).FirstOrDefault();
            var personel = db.TblPersonel.Where(x => x.id == p.TblPersonel.id).FirstOrDefault();

            p.TblUrunler = urun;
            p.TblMusteri = musteri;
            p.TblPersonel = personel;
            p.tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.TblSatislar.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}