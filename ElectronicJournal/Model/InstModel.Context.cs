﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ElectronicJournal.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class InstDBEntities1 : DbContext
    {
        public InstDBEntities1()
            : base("name=InstDBEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<addresses> addresses { get; set; }
        public virtual DbSet<employee_training> employee_training { get; set; }
        public virtual DbSet<employee_violation> employee_violation { get; set; }
        public virtual DbSet<employees> employees { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<trainings> trainings { get; set; }
        public virtual DbSet<USERS> USERS { get; set; }
        public virtual DbSet<violation_resolution> violation_resolution { get; set; }
        public virtual DbSet<violations> violations { get; set; }
    }
}