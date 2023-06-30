using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffRoadWorld.Data.Migrations
{
    public partial class AddedEngineHPandCC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EngineCapacity",
                table: "Vehicles",
                type: "int",
                maxLength: 5500,
                nullable: true,
                comment: "Engine capacity measured in cubic centimeters.");

            migrationBuilder.AddColumn<int>(
                name: "HorsePower",
                table: "Vehicles",
                type: "int",
                maxLength: 1000,
                nullable: false,
                defaultValue: 0,
                comment: "Maximum horsepower a vehicle can have.");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EngineCapacity",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HorsePower",
                table: "Vehicles");
        }
    }
}
