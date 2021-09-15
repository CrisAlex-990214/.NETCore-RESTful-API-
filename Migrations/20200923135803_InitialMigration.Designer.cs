﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shop.API.Entities;

namespace Shop.API.Migrations
{
    [DbContext(typeof(ProductRepositoryContext))]
    [Migration("20200923135803_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Shop.API.Entities.Product", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<DateTimeOffset>("purchasedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("status")
                        .HasColumnType("bit");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("value")
                        .HasColumnType("float");

                    b.HasKey("id");

                    b.ToTable("products");

                    b.HasData(
                        new
                        {
                            id = new Guid("8c99e7b8-6661-4ff6-944e-084ef3f562a6"),
                            description = "An apartment located in Cali, Colombia",
                            purchasedDate = new DateTimeOffset(new DateTime(2020, 9, 23, 8, 58, 2, 914, DateTimeKind.Unspecified).AddTicks(5475), new TimeSpan(0, -5, 0, 0, 0)),
                            status = true,
                            type = "Apartment",
                            value = 45000000.0
                        },
                        new
                        {
                            id = new Guid("1d0ab983-f9a6-445a-ae55-f89c5361408d"),
                            description = "Chevrolet Corvette from Europe",
                            purchasedDate = new DateTimeOffset(new DateTime(2020, 9, 23, 8, 58, 2, 917, DateTimeKind.Unspecified).AddTicks(2160), new TimeSpan(0, -5, 0, 0, 0)),
                            status = true,
                            type = "Vehicle",
                            value = 1200000000.0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
