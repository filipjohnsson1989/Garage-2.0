﻿// <auto-generated />
using System;
using Garage_2._0.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Garage_2._0.Migrations
{
    [DbContext(typeof(Garage_2_0Context))]
    partial class Garage_2_0ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Garage_2._0.Models.Entities.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CheckIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CheckOut")
                        .HasColumnType("datetime2");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("ParkingCost")
                        .HasColumnType("float");

                    b.Property<string>("RegNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VehicleType")
                        .HasColumnType("int");

                    b.Property<int>("Wheels")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Vehicle");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Brand = "Volvo",
                            CheckIn = new DateTime(2022, 2, 4, 18, 25, 52, 934, DateTimeKind.Local).AddTicks(5540),
                            Color = "Röd",
                            Model = "V70",
                            RegNo = "ABC123",
                            VehicleType = 0,
                            Wheels = 4
                        },
                        new
                        {
                            Id = 2,
                            Brand = "Messerschitt",
                            CheckIn = new DateTime(2022, 2, 4, 19, 10, 52, 934, DateTimeKind.Local).AddTicks(5620),
                            Color = "Silver",
                            Model = "KR 200",
                            RegNo = "ABC456",
                            VehicleType = 0,
                            Wheels = 3
                        },
                        new
                        {
                            Id = 3,
                            Brand = "Honda",
                            CheckIn = new DateTime(2022, 2, 4, 18, 55, 52, 934, DateTimeKind.Local).AddTicks(5624),
                            Color = "Svart",
                            Model = "CB 125T",
                            RegNo = "ABC789",
                            VehicleType = 4,
                            Wheels = 2
                        },
                        new
                        {
                            Id = 4,
                            Brand = "MAN",
                            CheckIn = new DateTime(2022, 2, 4, 19, 20, 52, 934, DateTimeKind.Local).AddTicks(5627),
                            Color = "Blå",
                            Model = "X-2000",
                            RegNo = "DEF123",
                            VehicleType = 2,
                            Wheels = 6
                        },
                        new
                        {
                            Id = 5,
                            Brand = "Nautor Swan",
                            CheckIn = new DateTime(2022, 1, 5, 19, 25, 52, 934, DateTimeKind.Local).AddTicks(5630),
                            Color = "Vit",
                            Model = "Swan 66",
                            RegNo = "SE456",
                            VehicleType = 1,
                            Wheels = 0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
