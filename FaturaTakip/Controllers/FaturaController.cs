using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FaturaTakip.Models;
using FaturaTakip.Models.ViewModels;

namespace FaturaTakip.Controllers
{
    
    public class FaturaController : Controller
    {
  
        FaturaTakipEntities db = new FaturaTakipEntities();


        int PageSize = 20;

        public ActionResult PersonelFaturaList(int page = 1)
        {
            var usId = (Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey;

            FaturaListViewModel faturaList = new FaturaListViewModel
            {

                FaturaList = db.AtananPersonel.Where(x => x.Fatura.isVisible == true && x.UserId == usId).OrderBy(n => n.Fatura.İncelendiMi.Value).ThenBy(x => x.Fatura.GonderimTarihi.Value).ToList().Select(x => x.Fatura).Skip((page - 1) * PageSize).Take(PageSize),
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

            //  [Authorize(Roles = "SatinAlma,MaliIsler")]

            // GET: Fatura
            public ActionResult Index(int page=1)
        {
            Boolean tumFaturalariGoster = false;
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity.Name;
                string[] roles = Roles.GetRolesForUser(user);



                foreach (var item in roles)
                {
                    if (item == "Admin" || item == "Santral" || item == "SatinAlma" || item == "MaliIsler")
                    {
                        tumFaturalariGoster = true;
                        break;
                    }

                }


                //CREATED BY FURKAN MERT SERTÇE 09.08.2019
                //FaturaListViewModel faturaList = new FaturaListViewModel();
                //faturaList.FaturaList = db.Fatura.OrderByDescending(x => x.GonderimTarihi.Value).ToList();
                if (tumFaturalariGoster == true)
                {
                    ViewBag.IncelenmisDosyaSayisi = db.Fatura.Where(x => x.FaturaInceleme.Id == 2 && x.isVisible == true).Count();
                    ViewBag.IncelenenDosyaSayisi = db.Fatura.Where(x => x.FaturaInceleme.Id == 1 && x.isVisible == true).Count();
                    ViewBag.IncelenmemisDosyaSayisi = db.Fatura.Where(x => x.FaturaInceleme.Id == 0 && x.isVisible == true).Count();

                    ViewBag.OnaylanmisDosyaSayisi = db.Fatura.Where(x => x.OnaylandiMi == true && x.isVisible == true).Count();
                    ViewBag.OnaylanmamisDosyaSayisi = db.Fatura.Where(x => x.OnaylandiMi == false || x.OnaylandiMi == null && x.isVisible == true).Count();

                    FaturaListViewModel faturaList = new FaturaListViewModel
                    {
                        FaturaList = db.Fatura.Where(x => x.isVisible == true).OrderBy(n => n.İncelendiMi.Value).ThenBy(x => x.GonderimTarihi.Value).ToList().Skip((page - 1) * PageSize).Take(PageSize),
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


                else
                {
                    return RedirectToAction("PersonelFaturaList");
                }
            }
            else
                return RedirectToAction("Index", "Login");

        }
        //https://www.youtube.com/watch?v=JeawBGzSZYU izle
        public JsonResult GetUsers(string searchTerm)
        {

            //List<string> allusers = (from MembershipUser c in Membership.GetAllUsers()
            //                         select new { UserName = c.ToString() }).Select(t => t.UserName).ToList());

            
            var dataList = db.aspnet_Users.ToList();
            if (searchTerm!=null)
            {
               dataList = db.aspnet_Users.Where(x => x.UserName.Contains(searchTerm)).ToList();

            }
            var modifiedData = dataList.Select(x => new
            {
                id = x.UserId,
                text = x.UserName
            });

            return Json(modifiedData, JsonRequestBehavior.AllowGet);
        }
     
        public JsonResult Save(string data)
        {

           
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Personeller(string data,string fatNo)
        {
            List<AtananPersonel> ap = db.AtananPersonel.Where(x => x.FatNo == fatNo).ToList();
            foreach (var item in ap)
            {
                db.AtananPersonel.Remove(item);
            }
       

          
            string[] authorsList = data.Split(',');
            foreach (var item in authorsList)
            {
                AtananPersonel atanan = new AtananPersonel();
                atanan.FatNo = fatNo;
                atanan.UserId= Guid.Parse(item);
                db.AtananPersonel.Add(atanan);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
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
          
           List<string> atananlar = db.AtananPersonel.Where(x => x.FatNo == id).Select(x=>x.aspnet_Users.UserName).ToList();
            string[] a = new String[atananlar.Count()];
            for (int i = 0; i < atananlar.Count(); i++)
            {
                a[i] = atananlar[i];
            }
            ViewData["FaturayaAtananlar"] = a;

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