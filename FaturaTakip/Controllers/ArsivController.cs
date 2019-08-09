using FaturaTakip.Models;
using FaturaTakip.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FaturaTakip.Controllers
{
    public class ArsivController : Controller
    {

        FaturaTakipEntities db = new FaturaTakipEntities();


        // GET: Fatura
        int PageSize = 20;
        [Authorize(Roles = "SatinAlma,MaliIsler")]
        public ActionResult Index(int page = 1)
        {

            //FaturaListViewModel faturaList = new FaturaListViewModel();
            //faturaList.FaturaList = db.Fatura.OrderByDescending(x => x.GonderimTarihi.Value).ToList();


            ViewBag.IncelenmisDosyaSayisi = db.Fatura.Where(x => x.FaturaInceleme.Id == 2 && x.isVisible == false).Count();
            ViewBag.IncelenenDosyaSayisi = db.Fatura.Where(x => x.FaturaInceleme.Id == 1 && x.isVisible == false).Count();
            ViewBag.IncelenmemisDosyaSayisi = db.Fatura.Where(x => x.FaturaInceleme.Id == 0 && x.isVisible == false).Count();

            ViewBag.OnaylanmisDosyaSayisi = db.Fatura.Where(x => x.OnaylandiMi == true && x.isVisible == false).Count();
            ViewBag.OnaylanmamisDosyaSayisi = db.Fatura.Where(x => x.OnaylandiMi != true && x.isVisible == false).Count();

            FaturaListViewModel faturaList = new FaturaListViewModel
            {
                FaturaList = db.Fatura.Where(x => x.isVisible == false).OrderBy(n => n.İncelendiMi.Value).ThenBy(x => x.GonderimTarihi.Value).ToList().Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = db.Fatura.Count()
                }
            };




            faturaList.EksikBilgiList = db.EksikBilgi.ToList();
            return View(faturaList);


        }

        [Authorize(Roles = "SatinAlma,MaliIsler")]
        public ActionResult Arsiv(string id)
        {
            Fatura f = db.Fatura.Find(id);
            f.isVisible = true;
            db.SaveChanges();
            return RedirectToAction("Index", "Arsiv");
        }

    }
}