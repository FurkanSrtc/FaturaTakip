
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
    
public partial class AtananPersonel
{

    public int id { get; set; }

    public string FatNo { get; set; }

    public Nullable<System.Guid> UserId { get; set; }



    public virtual aspnet_Users aspnet_Users { get; set; }

    public virtual Fatura Fatura { get; set; }

}

}
