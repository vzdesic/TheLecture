﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Enciklopedija.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EnciklopedijaEntities : DbContext
    {
        public EnciklopedijaEntities()
            : base("name=EnciklopedijaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Zanimljivost> Zanimljivosts { get; set; }
        public virtual DbSet<Zanr> Zanrs { get; set; }
        public virtual DbSet<Dogadaj> Dogadajs { get; set; }
        public virtual DbSet<Osoba> Osobas { get; set; }
        public virtual DbSet<Izvodac> Izvodacs { get; set; }
        public virtual DbSet<Pjesma> Pjesmas { get; set; }
    }
}
