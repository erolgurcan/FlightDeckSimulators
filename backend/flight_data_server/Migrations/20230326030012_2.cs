using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace flight_data_server.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PilotID",
                table: "UserData",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PilotID",
                table: "UserData");
        }
    }
}
