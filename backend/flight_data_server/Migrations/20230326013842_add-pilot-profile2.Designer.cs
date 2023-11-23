﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using flight_data_server.Database;

#nullable disable

namespace flight_data_server.Migrations
{
    [DbContext(typeof(UserDBContext))]
    [Migration("20230326013842_add-pilot-profile2")]
    partial class addpilotprofile2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("flight_data_server.Models.User.PilotProfileModel", b =>
                {
                    b.Property<int>("pilotId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("pilotId"), 1L, 1);

                    b.Property<string>("pilotRating")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userProfileID")
                        .HasColumnType("int");

                    b.HasKey("pilotId");

                    b.ToTable("PilotProfile");
                });

            modelBuilder.Entity("flight_data_server.Models.User.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("UserData");
                });
#pragma warning restore 612, 618
        }
    }
}
