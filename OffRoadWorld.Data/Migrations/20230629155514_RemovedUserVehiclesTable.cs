using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffRoadWorld.Data.Migrations
{
    public partial class RemovedUserVehiclesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserVehicles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserVehicles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Id of user"),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of Vehicle")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVehicles", x => new { x.UserId, x.VehicleId });
                    table.ForeignKey(
                        name: "FK_UserVehicles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserVehicles_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Vehicles of user.");

            migrationBuilder.CreateIndex(
                name: "IX_UserVehicles_VehicleId",
                table: "UserVehicles",
                column: "VehicleId");
        }
    }
}
