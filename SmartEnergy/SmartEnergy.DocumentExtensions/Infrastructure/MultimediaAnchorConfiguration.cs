using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEnergy.DocumentExtensions.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.DocumentExtensions.Infrastructure
{
    public class MultimediaAnchorConfiguration : IEntityTypeConfiguration<MultimediaAnchor>
    {
        public void Configure(EntityTypeBuilder<MultimediaAnchor> builder)
        {
            builder.HasKey(i => i.ID);

            builder.Property(i => i.ID)
                .ValueGeneratedOnAdd();
        }
    }
}
