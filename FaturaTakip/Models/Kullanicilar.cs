
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
    
public partial class Kullanicilar
{

    public int KNo { get; set; }

    public string Adi { get; set; }

    public string Soyadi { get; set; }

    public Nullable<byte> DepartmanNo { get; set; }

    public string KAdi { get; set; }

    public string Sifre { get; set; }



    public virtual Departman Departman { get; set; }

}

}
