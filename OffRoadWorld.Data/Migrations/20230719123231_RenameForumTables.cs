using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffRoadWorld.Data.Migrations
{
    public partial class RenameForumTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Threads_ThreadId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "Threads");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ThreadId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ThreadId",
                table: "Posts");

            migrationBuilder.AddColumn<Guid>(
                name: "TopicId",
                table: "Posts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "Topic id.");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Forums",
                type: "nvarchar(35)",
                maxLength: 35,
                nullable: false,
                comment: "Forum title.",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldComment: "Forum title.");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Forums",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "Forum description.",
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70,
                oldComment: "Forum description.");

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Topic id."),
                    Title = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false, comment: "Topic title."),
                    Content = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: false, comment: "Topic content."),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Topic owner"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Topic created at."),
                    ForumId = table.Column<int>(type: "int", nullable: false, comment: "Forum id.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topics_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Topics_Forums_ForumId",
                        column: x => x.ForumId,
                        principalTable: "Forums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Topic");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_TopicId",
                table: "Posts",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_ForumId",
                table: "Topics",
                column: "ForumId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_OwnerId",
                table: "Topics",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Topics_TopicId",
                table: "Posts",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Topics_TopicId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropIndex(
                name: "IX_Posts_TopicId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "Posts");

            migrationBuilder.AddColumn<Guid>(
                name: "ThreadId",
                table: "Posts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "Thread id.");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Forums",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                comment: "Forum title.",
                oldClrType: typeof(string),
                oldType: "nvarchar(35)",
                oldMaxLength: 35,
                oldComment: "Forum title.");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Forums",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                comment: "Forum description.",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "Forum description.");

            migrationBuilder.CreateTable(
                name: "Threads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Thread id."),
                    ForumId = table.Column<int>(type: "int", nullable: false, comment: "Forum id."),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Thread owner"),
                    Content = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: false, comment: "Thread content."),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Thread created at."),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Thread title.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Threads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Threads_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Threads_Forums_ForumId",
                        column: x => x.ForumId,
                        principalTable: "Forums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Thread");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ThreadId",
                table: "Posts",
                column: "ThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_Threads_ForumId",
                table: "Threads",
                column: "ForumId");

            migrationBuilder.CreateIndex(
                name: "IX_Threads_OwnerId",
                table: "Threads",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Threads_ThreadId",
                table: "Posts",
                column: "ThreadId",
                principalTable: "Threads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
