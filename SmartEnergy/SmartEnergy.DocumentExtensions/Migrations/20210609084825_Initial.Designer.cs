﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartEnergy.Infrastructure;

namespace SmartEnergy.DocumentExtensions.Migrations
{
    [DbContext(typeof(DocumentExtensionsDbContext))]
    [Migration("20210609084825_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SmartEnergy.DocumentExtensions.DomainModels.MultimediaAnchor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("ID");

                    b.ToTable("MultimediaAnchors");
                });

            modelBuilder.Entity("SmartEnergy.DocumentExtensions.DomainModels.MultimediaAttachment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MultimediaAnchorID")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("MultimediaAnchorID");

                    b.ToTable("MultimediaAttachments");
                });

            modelBuilder.Entity("SmartEnergy.DocumentExtensions.DomainModels.Notification", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<int>("NotificationAnchorID")
                        .HasColumnType("int");

                    b.Property<string>("NotificationType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("NotificationAnchorID");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("SmartEnergy.DocumentExtensions.DomainModels.NotificationAnchor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("ID");

                    b.ToTable("NotificationAnchors");
                });

            modelBuilder.Entity("SmartEnergy.DocumentExtensions.DomainModels.StateChangeAnchor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("ID");

                    b.ToTable("StateChangeAnchors");
                });

            modelBuilder.Entity("SmartEnergy.DocumentExtensions.DomainModels.StateChangeHistory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("ChangeDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("DocumentStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StateChangeAnchorID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("StateChangeAnchorID");

                    b.ToTable("StateChangeHistories");
                });

            modelBuilder.Entity("SmartEnergy.DocumentExtensions.DomainModels.MultimediaAttachment", b =>
                {
                    b.HasOne("SmartEnergy.DocumentExtensions.DomainModels.MultimediaAnchor", "MultimediaAnchor")
                        .WithMany("MultimediaAttachments")
                        .HasForeignKey("MultimediaAnchorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MultimediaAnchor");
                });

            modelBuilder.Entity("SmartEnergy.DocumentExtensions.DomainModels.Notification", b =>
                {
                    b.HasOne("SmartEnergy.DocumentExtensions.DomainModels.NotificationAnchor", "NotificationAnchor")
                        .WithMany("Notifications")
                        .HasForeignKey("NotificationAnchorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NotificationAnchor");
                });

            modelBuilder.Entity("SmartEnergy.DocumentExtensions.DomainModels.StateChangeHistory", b =>
                {
                    b.HasOne("SmartEnergy.DocumentExtensions.DomainModels.StateChangeAnchor", "StateChangeAnchor")
                        .WithMany("StateChangeHistories")
                        .HasForeignKey("StateChangeAnchorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StateChangeAnchor");
                });

            modelBuilder.Entity("SmartEnergy.DocumentExtensions.DomainModels.MultimediaAnchor", b =>
                {
                    b.Navigation("MultimediaAttachments");
                });

            modelBuilder.Entity("SmartEnergy.DocumentExtensions.DomainModels.NotificationAnchor", b =>
                {
                    b.Navigation("Notifications");
                });

            modelBuilder.Entity("SmartEnergy.DocumentExtensions.DomainModels.StateChangeAnchor", b =>
                {
                    b.Navigation("StateChangeHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
