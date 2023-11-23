using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace flight_data_server.Migrations.FlightDataDB
{
    public partial class addpilotnumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "pilotID",
                table: "FlightInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pilotID",
                table: "FlightInfo");
        }
    }
}
