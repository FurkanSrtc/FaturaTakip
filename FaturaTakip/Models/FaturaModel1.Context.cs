﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FaturaTakipEntities : DbContext
    {
        public FaturaTakipEntities()
            : base("name=FaturaTakipEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Departman> Departman { get; set; }
        public virtual DbSet<Firmalar> Firmalar { get; set; }
        public virtual DbSet<HataTuru> HataTuru { get; set; }
        public virtual DbSet<Kullanicilar> Kullanicilar { get; set; }
        public virtual DbSet<EksikBilgi> EksikBilgi { get; set; }
        public virtual DbSet<Fatura> Fatura { get; set; }
    }
}
