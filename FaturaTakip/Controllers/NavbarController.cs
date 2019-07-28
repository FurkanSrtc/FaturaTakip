using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FaturaTakip.Controllers
{
    public class NavbarController : Controller
    {
        // GET: Navbar
        [ChildActionOnly]
        public ActionResult Navbar()
        {
            return PartialView();
        }
    }
}