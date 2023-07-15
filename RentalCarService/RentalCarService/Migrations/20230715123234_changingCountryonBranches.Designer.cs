﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RentalCarService;

#nullable disable

namespace RentalCarService.Migrations
{
    [DbContext(typeof(RentalCarsDBContext))]
    [Migration("20230715123234_changingCountryonBranches")]
    partial class changingCountryonBranches
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RentalCarService.Models.Branchs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("RentalCarService.Models.Brands", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("RentalCarService.Models.Categories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("RentalCarService.Models.Countries", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("RentalCarService.Models.OpeningHours", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BranchsId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Closes")
                        .HasColumnType("time");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Opens")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("BranchsId");

                    b.ToTable("OpeningHours");
                });

            modelBuilder.Entity("RentalCarService.Models.PriceBands", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("MaxDays")
                        .HasColumnType("int");

                    b.Property<int>("MinDays")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CategoriesId");

                    b.ToTable("PriceBands");
                });

            modelBuilder.Entity("RentalCarService.Models.Branchs", b =>
                {
                    b.HasOne("RentalCarService.Models.Countries", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("RentalCarService.Models.OpeningHours", b =>
                {
                    b.HasOne("RentalCarService.Models.Branchs", null)
                        .WithMany("OpeningHours")
                        .HasForeignKey("BranchsId");
                });

            modelBuilder.Entity("RentalCarService.Models.PriceBands", b =>
                {
                    b.HasOne("RentalCarService.Models.Categories", null)
                        .WithMany("PriceBands")
                        .HasForeignKey("CategoriesId");
                });

            modelBuilder.Entity("RentalCarService.Models.Branchs", b =>
                {
                    b.Navigation("OpeningHours");
                });

            modelBuilder.Entity("RentalCarService.Models.Categories", b =>
                {
                    b.Navigation("PriceBands");
                });
#pragma warning restore 612, 618
        }
    }
}