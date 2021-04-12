using Fupi_WebApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fupi_WebApplication.DAL
{
    public class FupiAppContext: DbContext
    {
        public virtual DbSet<FupiModel> FupiModel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=host.docker.internal;Port=5432;Database=Fupi;Username=postgres;Password=j0ni@1;");
            //=> optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Fupi;Username=postgres;Password=j0ni@1;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FupiModel>().Property(p => p.Id).UseIdentityAlwaysColumn();
            modelBuilder.Entity<FupiModel>().HasIndex(x => x.ProxyCode).IsUnique();
            
            base.OnModelCreating(modelBuilder);
        }

    }
}
