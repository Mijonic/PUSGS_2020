using Microsoft.EntityFrameworkCore;
using SmartEnergy.Physical.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartEnergy.Physical.Infrastructure
{
    public class PhysicalDbContext : DbContext
    {
        public PhysicalDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<DeviceUsage> DeviceUsages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PhysicalDbContext).Assembly);

        }
    }
}
