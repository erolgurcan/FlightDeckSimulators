using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace flight_data_server.Migrations.FlightDataDB
{
    public partial class addmoredata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AOA",
                table: "FlightData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Throttle1",
                table: "FlightData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Throttle2",
                table: "FlightData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "landingGear",
                table: "FlightData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "outsideTemperature",
                table: "FlightData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "totalFuel",
                table: "FlightData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AOA",
                table: "FlightData");

            migrationBuilder.DropColumn(
                name: "Throttle1",
                table: "FlightData");

            migrationBuilder.DropColumn(
                name: "Throttle2",
                table: "FlightData");

            migrationBuilder.DropColumn(
                name: "landingGear",
                table: "FlightData");

            migrationBuilder.DropColumn(
                name: "outsideTemperature",
                table: "FlightData");

            migrationBuilder.DropColumn(
                name: "totalFuel",
                table: "FlightData");
        }
    }
}
