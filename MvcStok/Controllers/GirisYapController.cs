using MvcStok.Models.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using MvcStok.Models.Entity;
namespace MvcStok.Controllers
{
    public class GirisYapController : Controller
    {
        // GET: GirisYap
        DbMvcStokEntities db = new DbMvcStokEntities();
        public ActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Giris(TblAdmin p)
        {
            var bilgiler = db.TblAdmin.FirstOrDefault(x => x.kullanici == p.kullanici && x.sifre == p.sifre);

                if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.kullanici,false);
                return RedirectToAction("Index", "Anasayfa");
            }
            else
            {
                return View();
            }
        }
        public ActionResult CikisYap()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Giris","GirisYap");
        }
    }
}