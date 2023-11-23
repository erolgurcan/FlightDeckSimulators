using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace flight_data_server.Migrations.FlightDataDB
{
    public partial class removearrivalDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "arivalDateHour",
                table: "FlightInfo");

            migrationBuilder.RenameColumn(
                name: "departureDateHour",
                table: "FlightInfo",
                newName: "departureDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "departureDate",
                table: "FlightInfo",
                newName: "departureDateHour");

            migrationBuilder.AddColumn<DateTime>(
                name: "arivalDateHour",
                table: "FlightInfo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
