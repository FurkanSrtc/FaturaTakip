using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using FaturaTakip.Models;

namespace FaturaTakip.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();

        }

        FaturaTakipEntities db = new FaturaTakipEntities();



        [HttpPost]
        public ActionResult Index(Kullanici k, string hatirla)
        {
            bool sonuc = Membership.ValidateUser(k.KullaniciAdi, k.Parola);
            if (sonuc)
            {
                if (hatirla == "on") // beni hatırla seçildiyse
                {
                    FormsAuthentication.RedirectFromLoginPage(k.KullaniciAdi, true);
                }
                else
                {
                    FormsAuthentication.RedirectFromLoginPage(k.KullaniciAdi, false);
                }

                if (User.IsInRole("MaliIsler"))
                {
                    return RedirectToAction("Index", "Fatura");
                }
                else if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Home", "Login");
                }
                else
                    return RedirectToAction("Page", "Login");
            }
            ViewBag.Mesaj = "Kullanıcı Adı veye Parola Hatalı";
            return View();

        }

        public ActionResult Page()
        {
            return View();
        }

        public ActionResult Home()
        {
            MembershipUserCollection users = Membership.GetAllUsers();//veritabanındaki tüm kullanıcıarı çektik
            return View(users);
        }



        public ActionResult Ekle()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Ekle(Kullanici k,string hatirla)
        {

            Membership.CreateUser(k.KullaniciAdi, k.Parola, k.Email);
            return View();

        }
    }

}