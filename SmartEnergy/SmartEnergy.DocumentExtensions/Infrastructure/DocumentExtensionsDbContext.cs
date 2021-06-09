using Microsoft.EntityFrameworkCore;
using SmartEnergy.DocumentExtensions.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Infrastructure
{
    public class DocumentExtensionsDbContext : DbContext
    {
        public DocumentExtensionsDbContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<MultimediaAnchor> MultimediaAnchors { get; set; }
        public DbSet<MultimediaAttachment> MultimediaAttachments { get; set; }
        public DbSet<NotificationAnchor> NotificationAnchors { get; set; }
        public DbSet<StateChangeAnchor> StateChangeAnchors { get; set; }
        public DbSet<StateChangeHistory> StateChangeHistories { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DocumentExtensionsDbContext).Assembly);

        }
    }
}
