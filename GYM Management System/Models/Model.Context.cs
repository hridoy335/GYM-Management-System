﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GYM_Management_System.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class gym_managementEntities : DbContext
    {
        public gym_managementEntities()
            : base("name=gym_managementEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Attendence> Attendences { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ClientBill> ClientBills { get; set; }
        public virtual DbSet<ClientBillTransection> ClientBillTransections { get; set; }
        public virtual DbSet<ClientServiceList> ClientServiceLists { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<FoodPlan> FoodPlans { get; set; }
        public virtual DbSet<ProductBuying> ProductBuyings { get; set; }
        public virtual DbSet<ProductPlan> ProductPlans { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<ScheduleTime> ScheduleTimes { get; set; }
        public virtual DbSet<Sell> Sells { get; set; }
        public virtual DbSet<Servicess> Servicesses { get; set; }
        public virtual DbSet<Storage> Storages { get; set; }
    }
}