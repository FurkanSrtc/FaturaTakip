using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FaturaTakip.Models;
using FaturaTakip.Models.ViewModels;

namespace FaturaTakip.Models.ViewModels
{
    public class FaturaEditViewModel
    {
        public List<EksikBilgi> eksikBilgiler { get; set; }
        public Fatura Fatura { get; set; }
        public List<HataTuruViewModel> Hatalar { get; set; }
    }
}