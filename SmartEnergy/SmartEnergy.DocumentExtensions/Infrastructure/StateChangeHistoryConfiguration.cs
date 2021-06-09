using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEnergy.DocumentExtensions.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.DocumentExtensions.Infrastructure
{
    public class StateChangeHistoryConfiguration : IEntityTypeConfiguration<StateChangeHistory>
    {
        public void Configure(EntityTypeBuilder<StateChangeHistory> builder)
        {
            builder.HasKey(i => i.ID);

            builder.Property(i => i.ID)
                .ValueGeneratedOnAdd();

            // add unique
           builder.Property(i => i.ChangeDate)
                .IsRequired(false)
                .HasDefaultValueSql("getdate()");

            builder.Property(i => i.DocumentStatus)
                .HasConversion<String>()
                .IsRequired();

            builder.HasOne(i => i.StateChangeAnchor)
                 .WithMany(p => p.StateChangeHistories)
                 .HasForeignKey(i => i.StateChangeAnchorID)
                 .IsRequired();

            builder.Property(i => i.UserID)
                 .IsRequired();

        }
    }
}
