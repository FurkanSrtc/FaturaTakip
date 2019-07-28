using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FaturaTakip.Models.ViewModels
{
    public class FaturaEkleViewModel
    {
        public Fatura Fatura { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FaturaTarihi { get; set; }
    }
}