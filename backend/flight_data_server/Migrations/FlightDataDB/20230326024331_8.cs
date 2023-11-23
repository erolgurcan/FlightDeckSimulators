using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace flight_data_server.Migrations.FlightDataDB
{
    public partial class _8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tailNumber",
                table: "FlightInfo",
                newName: "TailNumber");

            migrationBuilder.RenameColumn(
                name: "departureAirportCode",
                table: "FlightInfo",
                newName: "DepartureAirportCode");

            migrationBuilder.RenameColumn(
                name: "arivalAirportCode",
                table: "FlightInfo",
                newName: "ArivalAirportCode");

            migrationBuilder.RenameColumn(
                name: "airplaneModel",
                table: "FlightInfo",
                newName: "AirplaneModel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TailNumber",
                table: "FlightInfo",
                newName: "tailNumber");

            migrationBuilder.RenameColumn(
                name: "DepartureAirportCode",
                table: "FlightInfo",
                newName: "departureAirportCode");

            migrationBuilder.RenameColumn(
                name: "ArivalAirportCode",
                table: "FlightInfo",
                newName: "arivalAirportCode");

            migrationBuilder.RenameColumn(
                name: "AirplaneModel",
                table: "FlightInfo",
                newName: "airplaneModel");
        }
    }
}
