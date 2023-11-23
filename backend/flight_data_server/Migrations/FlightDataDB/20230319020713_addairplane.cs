using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace flight_data_server.Migrations.FlightDataDB
{
    public partial class addairplane : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "airplaneModel",
                table: "FlightInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "airplaneModel",
                table: "FlightInfo");
        }
    }
}
