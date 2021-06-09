using Microsoft.EntityFrameworkCore;
using SmartEnergy.Users.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Infrastructure
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Consumer> Consumers { get; set; } 
        public DbSet<Crew> Crews { get; set; }
        public DbSet<User> Users { get; set; }
       



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsersDbContext).Assembly);

        }
    }
}
