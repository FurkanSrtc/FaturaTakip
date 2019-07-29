using FaturaTakip.Models;
using FaturaTakip.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FaturaTakip.Controllers
{
    public class MembershipController : Controller
    {
        // GET: Membership
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult RolAta(string id)
        {

            ViewBag.Roller = new SelectList(Roles.GetAllRoles().ToList());
            RolAta r = new RolAta();
            r.KullaniciAdi = id;
            return View(r);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult RolAta(RolAta r)
        {
            Roles.AddUserToRole(r.KullaniciAdi, r.Rol);

            return RedirectToAction("Home", "Login");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult uyeRolleri(string id)
        {
            Session["RolKullaniciAdi"] = id;
            ViewBag.roller = Roles.GetRolesForUser(id).ToList();
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Page", "Login");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UyeRolSil(RolAta r, string id)
        {
            Roles.RemoveUserFromRole((string)Session["RolKullaniciAdi"], id);
            return RedirectToAction("Page", "Login");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            Membership.DeleteUser(id);
            return RedirectToAction("Home", "Login");
        }


        public ActionResult Add()
        {
            return View();
        }

        // GET: Membership ÜYE OLMA EKRANI
        [HttpPost]
        public ActionResult Add(Kullanici k)
        {

            MembershipCreateStatus durum;
            Membership.CreateUser(k.KullaniciAdi, k.Parola, k.Email, k.GizliSoru, k.GizliCevap, true, out durum);

            string mesaj = "";
            switch (durum)
            {
                case MembershipCreateStatus.Success:
                    break;
                case MembershipCreateStatus.InvalidUserName:
                    mesaj += "Geçersiz Kullanıcı Adı";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    mesaj += "Geçersiz Parola";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    mesaj += "Geçersiz Gizli Soru";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    mesaj += "Geçersiz Gizli Cevap";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    mesaj += "Geçersiz Mail Adresi";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    mesaj += "Kullanılmış Kullanıcı Adı.";
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    mesaj += "Kullanılmış Mail Adresi Girildi.";
                    break;
                case MembershipCreateStatus.UserRejected:
                    mesaj += "Kullanıcı Engel Hatası";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    mesaj += "Geçersiz Kullanıcı Key Hatası.";
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    mesaj += "Kullanılmış Kullanıcı Key Hatası.";
                    break;
                case MembershipCreateStatus.ProviderError:
                    mesaj += "Üye yönetimi sağlayıcısı hatası";
                    break;
                default:
                    break;
            }
            ViewBag.Mesaj = mesaj;
            if (durum == MembershipCreateStatus.Success)
            {
                return RedirectToAction("Home", "Login");
            }
            return View();
        }
    }
}