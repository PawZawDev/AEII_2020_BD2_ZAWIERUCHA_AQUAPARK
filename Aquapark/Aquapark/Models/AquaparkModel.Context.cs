﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Aquapark.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Attraction> Attraction { get; set; }
        public virtual DbSet<EntryGate> EntryGate { get; set; }
        public virtual DbSet<ClientEntry> ClientEntry { get; set; }
        public virtual DbSet<Wristband> Wristband { get; set; }
        public virtual DbSet<TicketType> TicketType { get; set; }
        public virtual DbSet<TicketInPriceList> TicketInPriceList { get; set; }
        public virtual DbSet<ClientTicket> ClientTicket { get; set; }
    }
}
