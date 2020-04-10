using KSquare.DMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KSquare.DMS.Repositories.Context
{
    public class KSquareContext : DbContext
    {
        public KSquareContext()
    : base()
        {
            //this.ChangeTracker.QueryTrackingBehavior =QueryTrackingBehavior.NoTracking;
        }

        public KSquareContext(DbContextOptions options)
            : base(options)
        {
            //this.ChangeTracker.QueryTrackingBehavior =QueryTrackingBehavior.NoTracking;
            Database.SetCommandTimeout(20000);
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserCategory> UserCategory { get; set; }
        public virtual DbSet<UserAuth> UserAuth { get; set; }
        public virtual DbSet<Beneficiary> Beneficiary { get; set; }
        public virtual DbSet<Volunteer> Volunteer { get; set; }
        public virtual DbSet<Donation> Donation { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //  base.OnModelCreating(modelBuilder);
        //  modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //}
    }
}
