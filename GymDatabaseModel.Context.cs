﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace sali
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GymDatabaseEntitiess : DbContext
    {
        public GymDatabaseEntitiess()
            : base("name=GymDatabaseEntitiess")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Equipment> Equipments { get; set; }
        public virtual DbSet<Equipment_Rentals> Equipment_Rentals { get; set; }
        public virtual DbSet<Maintenance_Logs> Maintenance_Logs { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Membership_Plans> Membership_Plans { get; set; }
        public virtual DbSet<Private_Lessons> Private_Lessons { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }
    }
}