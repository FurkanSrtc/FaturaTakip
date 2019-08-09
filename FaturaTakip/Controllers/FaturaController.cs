using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FaturaTakip.Models;
using FaturaTakip.Models.ViewModels;

namespace FaturaTakip.Controllers
{
    public class FaturaController : Controller
    {
        FaturaTakipEntities db = new FaturaTakipEntities();


        int PageSize = 20;
        [Authorize(Roles = "SatinAlma,MaliIsler")]
     
        // GET: Fatura
        public ActionResult Index(int page=1)
        {

            //FaturaListViewModel faturaList = new FaturaListViewModel();
            //faturaList.FaturaList = db.Fatura.OrderByDescending(x => x.GonderimTarihi.Value).ToList();


            ViewBag.IncelenmisDosyaSayisi = db.Fatura.Where(x => x.FaturaInceleme.Id == 2 && x.isVisible==true).Count();
            ViewBag.IncelenenDosyaSayisi = db.Fatura.Where(x => x.FaturaInceleme.Id == 1 && x.isVisible == true).Count();
            ViewBag.IncelenmemisDosyaSayisi = db.Fatura.Where(x => x.FaturaInceleme.Id == 0 && x.isVisible == true).Count();

            ViewBag.OnaylanmisDosyaSayisi = db.Fatura.Where(x => x.OnaylandiMi == true && x.isVisible == true).Count();
            ViewBag.OnaylanmamisDosyaSayisi = db.Fatura.Where(x => x.OnaylandiMi == false || x.OnaylandiMi==null && x.isVisible == true).Count();

            FaturaListViewModel faturaList = new FaturaListViewModel
            {
                FaturaList = db.Fatura.Where(x=>x.isVisible==true).OrderBy(n => n.İncelendiMi.Value).ThenBy(x => x.GonderimTarihi.Value).ToList().Skip((page - 1) * PageSize).Take(PageSize),
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
        public ActionResult Edit(string id)
        {
            FaturaEditViewModel f = new FaturaEditViewModel();

            HataTuruViewModel hataTuruView = new HataTuruViewModel();


            List<FaturaInceleme> isDurumlari = db.FaturaInceleme.ToList();
            ViewBag.Inceleme = new SelectList(isDurumlari, "Id", "IncelemeTuru");


            List<EksikBilgi> eksikBilgiler = db.EksikBilgi.Where(x => x.FatNo == id).ToList();
            f.Hatalar = new List<HataTuruViewModel>();
            foreach (var item in db.HataTuru.ToList())
            {
                hataTuruView = new HataTuruViewModel();
                hataTuruView.HataAdi = item.HataAdi;
                hataTuruView.HKodu = item.HKodu;

                if (eksikBilgiler.Where(x => x.HataKodu == item.HKodu).FirstOrDefault()==null)
                {
                    hataTuruView.isChecked = false;
                }
                else
                { 
                    hataTuruView.isChecked = true;
                }

                f.Hatalar.Add(hataTuruView);
                
            }

            

            ViewBag.FaturaNo = id;

            f.Fatura = db.Fatura.Where(x => x.FaturaNo == id).FirstOrDefault();
           

            return View(f);
        }

        [Authorize(Roles = "SatinAlma,MaliIsler")]
        public ActionResult Arsiv(string id)
        {
            Fatura f = db.Fatura.Find(id);
            f.isVisible = false;
            db.SaveChanges();
            return RedirectToAction("Index", "Fatura");
        }

       

   


        [Authorize(Roles = "SatinAlma,MaliIsler")]
        [HttpPost]
        public ActionResult Edit(FaturaEditViewModel fe)
        {
            var f = db.Fatura.Find(fe.Fatura.FaturaNo);
            //f.FaturaNo = fe.Fatura.FaturaNo;
            f.Aciklama = fe.Fatura.Aciklama;
            //f.PdfYolu = fe.Fatura.PdfYolu;
            f.İncelendiMi = fe.Fatura.İncelendiMi;
            f.FaturaTarihi = fe.Fatura.FaturaTarihi;
            f.GonderimTarihi = DateTime.Now;
            f.isVisible = true;
            f.FirmaId = fe.Fatura.FirmaId;
            f.BilgisayarAdi = "";
            f.KullaniciNo = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(User.Identity.Name);
            f.OnaylandiMi = fe.Fatura.OnaylandiMi;

            foreach (var item in db.EksikBilgi.ToList().Where(x=>x.FatNo==fe.Fatura.FaturaNo))
            {
                db.EksikBilgi.Remove(item);
            }

         
            foreach (var item in fe.Hatalar)
            {
                if (item.isChecked==true)
                {
                    EksikBilgi eksik = new EksikBilgi();
                    eksik.FatNo = fe.Fatura.FaturaNo;
                    eksik.HataKodu = item.HKodu;
                    db.EksikBilgi.Add(eksik);
                }
            }

         
          
            db.SaveChanges();
            return RedirectToAction("Index", "Fatura");
        }
    }

}