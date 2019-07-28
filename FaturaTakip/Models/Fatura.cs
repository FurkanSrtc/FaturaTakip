//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FaturaTakip.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Fatura
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Fatura()
        {
            this.EksikBilgi = new HashSet<EksikBilgi>();
        }
    
        public string FaturaNo { get; set; }
        public Nullable<System.DateTime> GonderimTarihi { get; set; }
        public string Aciklama { get; set; }
        public string PdfYolu { get; set; }
        public Nullable<bool> İncelendiMi { get; set; }
        public Nullable<bool> isVisible { get; set; }
        public Nullable<int> FirmaId { get; set; }
        public Nullable<System.DateTime> FaturaTarihi { get; set; }
        public string KullaniciNo { get; set; }
        public string BilgisayarAdi { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EksikBilgi> EksikBilgi { get; set; }
        public virtual Firmalar Firmalar { get; set; }
    }
}
