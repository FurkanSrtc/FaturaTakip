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

        // GET: Fatura
        public ActionResult Index()
        {
            FaturaListViewModel faturaList = new FaturaListViewModel();
            faturaList.FaturaList = db.Fatura.ToList();
            faturaList.EksikBilgiList = db.EksikBilgi.ToList();
            return View(faturaList);
        }

        
        public ActionResult Edit(string id)
        {
            FaturaEditViewModel f = new FaturaEditViewModel();

            HataTuruViewModel hataTuruView = new HataTuruViewModel();

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