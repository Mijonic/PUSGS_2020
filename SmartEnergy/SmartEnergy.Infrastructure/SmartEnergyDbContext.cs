using Microsoft.EntityFrameworkCore;
using SmartEnergy.Infrastructure.Configurations;
using SmartEnergyDomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Infrastructure
{
    public class SmartEnergyDbContext : DbContext
    {
        public SmartEnergyDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Settings> Settings { get; set; }
        public DbSet<Icon> Icons { get; set; }

        public DbSet<Consumer> Consumers { get; set; }

        public DbSet<Call> Calls { get; set; }
        public DbSet<Crew> Crews { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Location> Locations { get; set; }

        public DbSet<Resolution> Resolutions { get; set; }

        public DbSet<User> Users { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new IconConfiguration());
            modelBuilder.ApplyConfiguration(new SettingsConfiguration());
            modelBuilder.ApplyConfiguration(new ConsumerConfiguration());
            modelBuilder.ApplyConfiguration(new CallConfiguration());
            modelBuilder.ApplyConfiguration(new CrewConfiguration());
            modelBuilder.ApplyConfiguration(new DeviceConfiguration());
            modelBuilder.ApplyConfiguration(new IncidentConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration());
            modelBuilder.ApplyConfiguration(new ResolutionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());



            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(SmartEnergyDbContext).Assembly);

        }
    }
}
