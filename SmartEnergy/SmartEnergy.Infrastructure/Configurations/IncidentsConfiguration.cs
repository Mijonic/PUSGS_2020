using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEnergyDomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Infrastructure.Configurations
{
    public class IncidentConfiguration : IEntityTypeConfiguration<Incident>
    {
        public void Configure(EntityTypeBuilder<Incident> builder)
        {
            //Set table name mapping
            // builder.ToTable("ListItems");

            //Set FK
            builder.HasKey(i => i.ID);


            builder.Property(i => i.WorkType)
                .HasConversion<String>()
                .IsRequired();

            builder.Property(i => i.Priority)
                .IsRequired(false);

            builder.Property(i => i.Confirmed)
                 .IsRequired();

            builder.Property(i => i.IncidentStatus)
                 .HasConversion<String>()
                 .IsRequired();

            builder.Property(i => i.ETA)
                 .IsRequired();

            builder.Property(i => i.ATA)
                  .IsRequired(false);

            builder.Property(i => i.IncidentDateTime)
                  .IsRequired(false);

            builder.Property(i => i.ETR)
                  .IsRequired(false);

            builder.Property(i => i.VoltageLevel)
                   .IsRequired(false);
             
            builder.Property(i => i.WorkBeginDate)
                  .IsRequired(false);


            builder.HasOne(i => i.User)
                .WithMany(p => p.Incidents)
                .HasForeignKey(i => i.UserID)
                .IsRequired();

            builder.HasOne(i => i.WorkRequest)
                .WithOne(p => p.Incident)
                .HasForeignKey<Incident>(i => i.WorkRequestID)
                .HasForeignKey<WorkRequest>(i => i.IncidentID)
                .IsRequired(false);

            builder.HasOne(i => i.Crew)
                .WithMany(p => p.Incidents)
                .HasForeignKey(i => i.CrewID)
                .IsRequired(false);

            builder.HasOne(i => i.MultimediaAnchor)
                .WithOne(p => p.Incident)
                .HasForeignKey<Incident>(i => i.MultimediaAnchorID)
                .HasForeignKey<MultimediaAnchor>(i => i.IncidentID)
                .IsRequired();

            builder.HasOne(i => i.NotificationAnchor)
               .WithOne(p => p.Incident)
               .HasForeignKey<Incident>(i => i.NotificationAnchorID)
               .HasForeignKey<NotificationAnchor>(i => i.IncidentID)
               .IsRequired();























        }
    }
}
