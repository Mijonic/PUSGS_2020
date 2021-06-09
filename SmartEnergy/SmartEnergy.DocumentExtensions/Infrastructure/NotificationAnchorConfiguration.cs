using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEnergy.DocumentExtensions.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.DocumentExtensions.Infrastructure
{
    public class NotificationAnchorConfiguration : IEntityTypeConfiguration<NotificationAnchor>
    {
        public void Configure(EntityTypeBuilder<NotificationAnchor> builder)
        {
            builder.HasKey(i => i.ID);

            builder.Property(i => i.ID)
                .ValueGeneratedOnAdd();
        }
    }
}
