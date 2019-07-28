using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FaturaTakip.Models.ViewModels
{
    public class HataTuruViewModel
    {
        public byte HKodu { get; set; }

        public string HataAdi { get; set; }

        public bool isChecked { get; set; }
    }

}