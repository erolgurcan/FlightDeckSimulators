using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace flight_data_server.Migrations.FlightDataDB
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AirlinerID",
                table: "FlightData");

            migrationBuilder.DropColumn(
                name: "FlightID",
                table: "FlightData");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AirlinerID",
                table: "FlightData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FlightID",
                table: "FlightData",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
