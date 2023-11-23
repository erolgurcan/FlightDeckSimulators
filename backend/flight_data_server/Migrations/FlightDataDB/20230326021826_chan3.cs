using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace flight_data_server.Migrations.FlightDataDB
{
    public partial class chan3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "totalFuel",
                table: "FlightData",
                newName: "TotalFuel");

            migrationBuilder.RenameColumn(
                name: "outsideTemperature",
                table: "FlightData",
                newName: "OutsideTemperature");

            migrationBuilder.RenameColumn(
                name: "landingGear",
                table: "FlightData",
                newName: "LandingGear");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalFuel",
                table: "FlightData",
                newName: "totalFuel");

            migrationBuilder.RenameColumn(
                name: "OutsideTemperature",
                table: "FlightData",
                newName: "outsideTemperature");

            migrationBuilder.RenameColumn(
                name: "LandingGear",
                table: "FlightData",
                newName: "landingGear");
        }
    }
}
