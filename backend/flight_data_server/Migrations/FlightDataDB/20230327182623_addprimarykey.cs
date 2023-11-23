using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace flight_data_server.Migrations.FlightDataDB
{
    public partial class addprimarykey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FlightInfo",
                table: "FlightInfo");

            migrationBuilder.AlterColumn<string>(
                name: "flightCode",
                table: "FlightInfo",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlightInfo",
                table: "FlightInfo",
                column: "flightCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FlightInfo",
                table: "FlightInfo");

            migrationBuilder.AlterColumn<string>(
                name: "flightCode",
                table: "FlightInfo",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlightInfo",
                table: "FlightInfo",
                column: "id");
        }
    }
}
