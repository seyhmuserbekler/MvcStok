using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;                               // Sayfalama kullanabilmek için PagedList ekledik
using PagedList.Mvc;
namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index(int sayfa=1)            //Kategorileri Listeleme İşlemi
        {

            var ktgr=db.TblKategori.ToList().ToPagedList(sayfa, 5);   
            return View(ktgr);
        }

        [HttpGet]
        public ActionResult YeniKategori() 
        { 
            return View();
        }

        [HttpPost]
        public ActionResult YeniKategori(TblKategori p)    //Kategorileri Ekleme İşlemi
        {
            db.TblKategori.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)                 //Kategorileri Silme  İşlemi
        {
            var ktgr= db.TblKategori.Find(id);
            db.TblKategori.Remove(ktgr);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public ActionResult KategoriGetir(int id)         //Kategorileri Güncelleme Sayfasına Taşıma İşlemi
        {
            var ktgr = db.TblKategori.Find(id);
          
            return View("KategoriGetir",ktgr);
        }

        public ActionResult KategoriGuncelle(TblKategori k)          //Kategorileri Güncelleme   İşlemi
        {
            var ktgr = db.TblKategori.Find(k.id);
            ktgr.ad=k.ad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}