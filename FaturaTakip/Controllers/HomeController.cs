using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using FaturaTakip.Models;
using FaturaTakip.Models.ViewModels;

namespace FaturaTakip.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Santral,SatinAlma,MaliIsler")]
        public ActionResult Ekle()
        {
            return View();
        }

        FaturaTakipEntities db = new FaturaTakipEntities();

        [Authorize(Roles = "Santral,SatinAlma,MaliIsler")]
        [HttpPost]
        public ActionResult Ekle(FaturaEkleViewModel f, HttpPostedFileBase file)
        {

            Regex rgx = new Regex(@"[^a-zA-Z]");
            string firmaAdi =rgx.Replace(f.Fatura.Firmalar.FirmaAdi, "");


            Fatura fatura = new Fatura();


            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string fn = f.Fatura.FaturaNo +"-"+ f.FaturaTarihi.ToShortDateString()+"-" + firmaAdi 
                        +".pdf";
                    
                    file.SaveAs(HttpContext.Server.MapPath("~/Pdf/") + fn);
                   fatura.PdfYolu =fn;

                }

                fatura.GonderimTarihi = DateTime.Now;
                fatura.Aciklama = "";
                fatura.İncelendiMi = 0;
                fatura.isVisible = true;
                fatura.FaturaTarihi = f.FaturaTarihi;
                fatura.KullaniciNo = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(User.Identity.Name);
                fatura.BilgisayarAdi = "";
                fatura.FaturaNo = f.Fatura.FaturaNo;
                fatura.OnaylandiMi = false;

                Firmalar firma = db.Firmalar.Where(x => x.FirmaAdi == f.Fatura.Firmalar.FirmaAdi).FirstOrDefault();

                if (firma==null || firma.FirmaAdi==null)
                {
                    firma = new Firmalar();
                    firma.FirmaAdi = f.Fatura.Firmalar.FirmaAdi;
                    db.Firmalar.Add(firma);
                    db.SaveChanges();
                    fatura.FirmaId = db.Firmalar.Max(x => x.FId);
                    db.Fatura.Add(fatura);
                    db.SaveChanges();
                }
                else
                {
                    fatura.FirmaId = firma.FId;
                    db.Fatura.Add(fatura);
                    db.SaveChanges();
                }

                // db.Fatura.Add(fatura);
          
                return RedirectToAction("Index","Fatura");
            }
            return View();

        }

        public JsonResult GetSearchValue(string search)
        {
           
            List<FirmaList> allsearch = db.Firmalar.Where(x => x.FirmaAdi.Contains(search)).Select(x => new FirmaList
            {
                FId = x.FId,
                FirmaAdi = x.FirmaAdi
            }).ToList();
            return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}