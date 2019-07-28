using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FaturaTakip.Models.ViewModels
{
    public class FaturaViewModel
    {

        public string FaturaNo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime GonderimTarihi { get; set; }

        [Required(ErrorMessage = "Bu Alanı Doldurmalısınız.")]
        [DisplayName("Açıklama")]
        public string Aciklama { get; set; }

        [DisplayName("Pdf Yolu")]
        public string PdfYolu { get; set; }
        public bool İncelendiMi { get; set; }
        public bool isVisible { get; set; }
        public int FirmaId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FaturaTarihi { get; set; }
        public int KullaniciNo { get; set; }
        public string BilgisayarAdi { get; set; }

    }
}