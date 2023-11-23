using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace flight_data_server.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userProfileID",
                table: "PilotProfile",
                newName: "UserProfileID");

            migrationBuilder.RenameColumn(
                name: "pilotRating",
                table: "PilotProfile",
                newName: "PilotRating");

            migrationBuilder.RenameColumn(
                name: "pilotId",
                table: "PilotProfile",
                newName: "PilotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserProfileID",
                table: "PilotProfile",
                newName: "userProfileID");

            migrationBuilder.RenameColumn(
                name: "PilotRating",
                table: "PilotProfile",
                newName: "pilotRating");

            migrationBuilder.RenameColumn(
                name: "PilotId",
                table: "PilotProfile",
                newName: "pilotId");
        }
    }
}
