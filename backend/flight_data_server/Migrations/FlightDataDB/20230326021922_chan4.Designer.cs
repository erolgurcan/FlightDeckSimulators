﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using flight_data_server.Database;

#nullable disable

namespace flight_data_server.Migrations.FlightDataDB
{
    [DbContext(typeof(FlightDataDBContext))]
    [Migration("20230326021922_chan4")]
    partial class chan4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("flight_data_server.Models.FlightData.FlightData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("AOA")
                        .HasColumnType("float");

                    b.Property<int>("AirlinerID")
                        .HasColumnType("int");

                    b.Property<double>("Altitude")
                        .HasColumnType("float");

                    b.Property<string>("FlightCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FlightID")
                        .HasColumnType("int");

                    b.Property<double>("GroundSpeed")
                        .HasColumnType("float");

                    b.Property<double>("Heading")
                        .HasColumnType("float");

                    b.Property<int>("LandingGear")
                        .HasColumnType("int");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<DateTime>("LoggingTime")
                        .HasColumnType("datetime2");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<double>("OutsideTemperature")
                        .HasColumnType("float");

                    b.Property<double>("Throttle")
                        .HasColumnType("float");

                    b.Property<double>("Throttle1")
                        .HasColumnType("float");

                    b.Property<double>("Throttle2")
                        .HasColumnType("float");

                    b.Property<double>("TotalFuel")
                        .HasColumnType("float");

                    b.Property<double>("TrueAirSpeed")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("FlightData");
                });

            modelBuilder.Entity("flight_data_server.Models.FlightData.FlightInfo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("airliner")
                        .HasColumnType("int");

                    b.Property<string>("airplaneModel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("arivalAirportCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("departureAirportCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("departureDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("flightCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<int>("pilotID")
                        .HasColumnType("int");

                    b.Property<string>("tailNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("FlightInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
