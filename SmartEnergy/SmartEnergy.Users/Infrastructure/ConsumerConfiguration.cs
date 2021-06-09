﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEnergy.Users.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Users.Infrastructure
{
    public class ConsumerConfiguration : IEntityTypeConfiguration<Consumer>
    {
        public void Configure(EntityTypeBuilder<Consumer> builder)
        {
            //Set table name mapping
            // builder.ToTable("ListItems");

            //Set FK
            builder.HasKey(i => i.ID);

            builder.Property(i => i.ID)
                .ValueGeneratedOnAdd();

            builder.Property(i => i.Name)
                .IsRequired(true)
                .HasMaxLength(30);

            builder.Property(i => i.Lastname)
               .IsRequired(true)
               .HasMaxLength(30);

            builder.Property(i => i.Phone)
               .IsRequired(true)
               .HasMaxLength(30);

            builder.Property(i => i.AccountID)
               .IsRequired(true);

            builder.Property(i => i.AccountType)
                .IsRequired(true);

            builder.HasOne(i => i.User)
                .WithOne(p => p.Consumer)
                .HasForeignKey<Consumer>(i => i.UserID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(i => i.LocationID)
                .IsRequired();

        }
    }
}
