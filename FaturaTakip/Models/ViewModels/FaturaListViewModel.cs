using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FaturaTakip.Models.ViewModels
{
    public class FaturaListViewModel
    {
        public List<Fatura> FaturaList { get; set; }

        public List<EksikBilgi> EksikBilgiList { get; set; }
    }
}