﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoEtDash.Web.Database.Models;

#nullable disable

namespace StoEtDash.Web.Migrations
{
    [DbContext(typeof(StoEtDashContext))]
    partial class StoEtDashContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("StoEtDash.Web.Database.Models.Transaction", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("VARCHAR (36)");

                    b.Property<int>("Action")
                        .HasColumnType("VARCHAR (4)");

                    b.Property<int>("Currency")
                        .HasColumnType("VARCHAR (3)");

                    b.Property<double>("ExchangeRate")
                        .HasColumnType("DOUBLE");

                    b.Property<double>("FeesInEur")
                        .HasColumnType("DOUBLE");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR (75)");

                    b.Property<double>("NumberOfShares")
                        .HasColumnType("DOUBLE");

                    b.Property<double>("PricePerShare")
                        .HasColumnType("DOUBLE");

                    b.Property<string>("Ticker")
                        .IsRequired()
                        .HasColumnType("VARCHAR (10)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("DATETIME");

                    b.Property<double>("TotalInEur")
                        .HasColumnType("DOUBLE");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("VARCHAR (60)");

                    b.HasKey("Id");

                    b.HasIndex("Username");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("StoEtDash.Web.Database.Models.User", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("VARCHAR (60)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("VARCHAR (128)");

                    b.HasKey("Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("StoEtDash.Web.Database.Models.Transaction", b =>
                {
                    b.HasOne("StoEtDash.Web.Database.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
