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
    [Migration("20230725133442_AddingBook")]
    partial class AddingBook
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RentalCarService.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BranchId")
                        .HasColumnType("int");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<int?>("ExtraId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ValueToPay")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ExtraId");

                    b.HasIndex("UserId");

                    b.ToTable("Books");
                });

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

            modelBuilder.Entity("RentalCarService.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AirConditioner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("BrandId")
                        .HasColumnType("int");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("Doors")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumberPlate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Seats")
                        .HasColumnType("int");

                    b.Property<string>("Transmission")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrunkSize")
                        .HasColumnType("int");

                    b.Property<DateTime>("Year")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Fleet");
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

            modelBuilder.Entity("RentalCarService.Models.Extraa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DayCost")
                        .HasColumnType("int");

                    b.Property<int>("FixedCost")
                        .HasColumnType("int");

                    b.Property<string>("Service")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Extras");
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

            modelBuilder.Entity("RentalCarService.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("CNH")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CountryCNHId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCNH")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentityDocument")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NationalityId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("CountryCNHId");

                    b.HasIndex("NationalityId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RentalCarService.Models.UserAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Neighborhood")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("UserAddress");
                });

            modelBuilder.Entity("RentalCarService.Models.Book", b =>
                {
                    b.HasOne("RentalCarService.Models.Branchs", "Branch")
                        .WithMany()
                        .HasForeignKey("BranchId");

                    b.HasOne("RentalCarService.Models.Categories", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("RentalCarService.Models.Extraa", "Extra")
                        .WithMany()
                        .HasForeignKey("ExtraId");

                    b.HasOne("RentalCarService.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Branch");

                    b.Navigation("Category");

                    b.Navigation("Extra");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RentalCarService.Models.Branchs", b =>
                {
                    b.HasOne("RentalCarService.Models.Countries", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("RentalCarService.Models.Car", b =>
                {
                    b.HasOne("RentalCarService.Models.Brands", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId");

                    b.HasOne("RentalCarService.Models.Categories", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.Navigation("Brand");

                    b.Navigation("Category");
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

            modelBuilder.Entity("RentalCarService.Models.User", b =>
                {
                    b.HasOne("RentalCarService.Models.UserAddress", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("RentalCarService.Models.Countries", "CountryCNH")
                        .WithMany()
                        .HasForeignKey("CountryCNHId");

                    b.HasOne("RentalCarService.Models.Countries", "Nationality")
                        .WithMany()
                        .HasForeignKey("NationalityId");

                    b.Navigation("Address");

                    b.Navigation("CountryCNH");

                    b.Navigation("Nationality");
                });

            modelBuilder.Entity("RentalCarService.Models.UserAddress", b =>
                {
                    b.HasOne("RentalCarService.Models.Countries", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.Navigation("Country");
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
