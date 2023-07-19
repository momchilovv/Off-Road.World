using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffRoadWorld.Data.Migrations
{
    public partial class ChangedForumDesctiptionMaxLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Forums",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                comment: "Forum description.",
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45,
                oldComment: "Forum description.");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Forums",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: false,
                comment: "Forum description.",
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70,
                oldComment: "Forum description.");
        }
    }
}
