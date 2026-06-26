using System;
using System.Collections.Generic;
using System.Text;
using Dsw2026Ej15.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data.Context
{
    public class DswDbContext : DbContext
    {
        public DswDbContext(DbContextOptions<DswDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Speciality> Specialities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Doctor>()
                .ToTable("Doctors");
            modelBuilder.Entity<Speciality>()
                .ToTable("Specialities");
        }
    }
}
