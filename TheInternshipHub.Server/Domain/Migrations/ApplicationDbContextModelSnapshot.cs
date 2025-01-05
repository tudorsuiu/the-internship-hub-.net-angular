﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheInternshipHub.Server.Domain.SmartService.Domain;

#nullable disable

namespace TheInternshipHub.Server.Domain.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TheInternshipHub.Server.Domain.Entities.APPLICATION", b =>
                {
                    b.Property<Guid>("AP_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AP_APPLIED_DATE")
                        .HasColumnType("datetime2");

                    b.Property<string>("AP_CV_FILE_PATH")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("AP_INTERNSHIP_ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("AP_IS_DELETED")
                        .HasColumnType("bit");

                    b.Property<string>("AP_STATUS")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("AP_STUDENT_ID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AP_ID");

                    b.HasIndex("AP_INTERNSHIP_ID");

                    b.HasIndex("AP_STUDENT_ID");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("TheInternshipHub.Server.Domain.Entities.COMPANY", b =>
                {
                    b.Property<Guid>("CO_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CO_CITY")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CO_LOGO_PATH")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CO_NAME")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CO_WEBSITE")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CO_ID");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("TheInternshipHub.Server.Domain.Entities.INTERNSHIP", b =>
                {
                    b.Property<Guid>("IN_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IN_COMPANY_ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("IN_COMPENSATION")
                        .HasColumnType("int");

                    b.Property<DateTime>("IN_DEADLINE")
                        .HasColumnType("datetime2");

                    b.Property<string>("IN_DESCRIPTION")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("IN_END_DATE")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IN_IS_DELETED")
                        .HasColumnType("bit");

                    b.Property<int>("IN_POSITIONS_AVAILABLE")
                        .HasColumnType("int");

                    b.Property<Guid>("IN_RECRUITER_ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("IN_START_DATE")
                        .HasColumnType("datetime2");

                    b.Property<string>("IN_TITLE")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("IN_ID");

                    b.HasIndex("IN_COMPANY_ID");

                    b.HasIndex("IN_RECRUITER_ID");

                    b.ToTable("Internships");
                });

            modelBuilder.Entity("TheInternshipHub.Server.Domain.Entities.USER", b =>
                {
                    b.Property<Guid>("US_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("US_CITY")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("US_COMPANY_ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("US_EMAIL")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("US_FIRST_NAME")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("US_IS_DELETED")
                        .HasColumnType("bit");

                    b.Property<string>("US_LAST_NAME")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("US_PASSWORD")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("US_PHONE_NUMBER")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("US_ROLE")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("US_ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TheInternshipHub.Server.Domain.Entities.APPLICATION", b =>
                {
                    b.HasOne("TheInternshipHub.Server.Domain.Entities.INTERNSHIP", "Internship")
                        .WithMany()
                        .HasForeignKey("AP_INTERNSHIP_ID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TheInternshipHub.Server.Domain.Entities.USER", "Student")
                        .WithMany()
                        .HasForeignKey("AP_STUDENT_ID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Internship");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("TheInternshipHub.Server.Domain.Entities.INTERNSHIP", b =>
                {
                    b.HasOne("TheInternshipHub.Server.Domain.Entities.COMPANY", "Company")
                        .WithMany()
                        .HasForeignKey("IN_COMPANY_ID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TheInternshipHub.Server.Domain.Entities.USER", "Recruiter")
                        .WithMany()
                        .HasForeignKey("IN_RECRUITER_ID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Recruiter");
                });
#pragma warning restore 612, 618
        }
    }
}
