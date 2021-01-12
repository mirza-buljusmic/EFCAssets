﻿// <auto-generated />
using System;
using EFCAssets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCAssets.Migrations
{
    [DbContext(typeof(AssetContext))]
    [Migration("20210112114132_ChangingAssetPriceFromDecToInt")]
    partial class ChangingAssetPriceFromDecToInt
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("EFCAssets.Models.Asset", b =>
                {
                    b.Property<int>("AssetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("AssetActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("AssetExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("AssetName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AssetPrice")
                        .HasColumnType("int");

                    b.Property<DateTime>("AssetPurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("AssetWarningDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("OfficeId")
                        .HasColumnType("int");

                    b.HasKey("AssetId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("OfficeId");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("EFCAssets.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CategoryEOLMonths")
                        .HasColumnType("int");

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("EFCAssets.Models.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CurrencyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("CurrensyToUSD")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("EFCAssets.Models.Office", b =>
                {
                    b.Property<int>("OfficeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CurrencyId")
                        .HasColumnType("int");

                    b.Property<string>("OfficeCountry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OfficeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OfficeId");

                    b.HasIndex("CurrencyId");

                    b.ToTable("Offices");
                });

            modelBuilder.Entity("EFCAssets.Models.Asset", b =>
                {
                    b.HasOne("EFCAssets.Models.Category", "Category")
                        .WithMany("Assets")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EFCAssets.Models.Office", "Office")
                        .WithMany("Assets")
                        .HasForeignKey("OfficeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Office");
                });

            modelBuilder.Entity("EFCAssets.Models.Office", b =>
                {
                    b.HasOne("EFCAssets.Models.Currency", "Currency")
                        .WithMany("Offices")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("EFCAssets.Models.Category", b =>
                {
                    b.Navigation("Assets");
                });

            modelBuilder.Entity("EFCAssets.Models.Currency", b =>
                {
                    b.Navigation("Offices");
                });

            modelBuilder.Entity("EFCAssets.Models.Office", b =>
                {
                    b.Navigation("Assets");
                });
#pragma warning restore 612, 618
        }
    }
}
