using Fupi_KeyGenService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fupi_KeyGenService.DAL
{
    public class KeyGenContext : DbContext
    {
        public virtual DbSet<KeyGenModel> KeyGenModel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=host.docker.internal;Port=5432;Database=Fupi;Username=postgres;Password=j0ni@1;");
        //=> optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Fupi;Username=postgres;Password=j0ni@1;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KeyGenModel>().Property(b => b.Id).UseIdentityAlwaysColumn();
            modelBuilder.Entity<KeyGenModel>().HasIndex(p => p.Proxy).IsUnique();
            modelBuilder.Entity<KeyGenModel>().Property(v => v.CreatedTime).ValueGeneratedOnAdd();

            modelBuilder.Entity<KeyGenModel>().Property(x => x.IsUtilized).HasDefaultValue("FALSE");

            modelBuilder.Entity<KeyGenModel>().HasData(
                new KeyGenModel { Id = int.MaxValue, Proxy = "boEuPq2qe", CreatedTime = DateTime.Now, IsUtilized = false },
                new KeyGenModel { Id = int.MaxValue - 1, Proxy = "pIR76TDof", CreatedTime = DateTime.Now, IsUtilized = false },
                new KeyGenModel { Id = int.MaxValue - 2, Proxy = "kOd4RqemF", CreatedTime = DateTime.Now, IsUtilized = false },
                new KeyGenModel { Id = int.MaxValue - 3, Proxy = "lUts45SD0", CreatedTime = DateTime.Now, IsUtilized = false }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
