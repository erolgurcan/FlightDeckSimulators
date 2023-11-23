using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace flight_data_server.Migrations.FlightDataDB
{
    public partial class addFlightInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlightInfo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    flightCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    airliner = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    departureDateHour = table.Column<DateTime>(type: "datetime2", nullable: false),
                    arivalDateHour = table.Column<DateTime>(type: "datetime2", nullable: false),
                    arivalAirportCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    departureAirportCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightInfo", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightInfo");
        }
    }
}
