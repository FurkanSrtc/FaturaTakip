using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FaturaTakip.Controllers
{
    public class RoleController : Controller
    {

        public ActionResult Index()
        {
            List<string> roller = Roles.GetAllRoles().ToList();
            return View(roller);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string RolAdi)
        {
            Roles.CreateRole(RolAdi);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            Roles.DeleteRole(id);
            return RedirectToAction("Index");
        }
    }
}