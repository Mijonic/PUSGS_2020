using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEnergy.DocumentExtensions.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.DocumentExtensions.Infrastructure
{
    public class StateChangeAnchorConfiguration : IEntityTypeConfiguration<StateChangeAnchor>
    {
        public void Configure(EntityTypeBuilder<StateChangeAnchor> builder)
        {
            builder.HasKey(i => i.ID);

            builder.Property(i => i.ID)
                .ValueGeneratedOnAdd();
        }
    }
}
