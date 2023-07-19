using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffRoadWorld.Data.Migrations
{
    public partial class AddingForumThreadsAndPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Forums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Forum id.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Forum title."),
                    Description = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false, comment: "Forum description.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forums", x => x.Id);
                },
                comment: "Forum");

            migrationBuilder.CreateTable(
                name: "Threads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Thread id."),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Thread title."),
                    Content = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: false, comment: "Thread content."),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Thread owner"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Thread created at."),
                    ForumId = table.Column<int>(type: "int", nullable: false, comment: "Forum id.")
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

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Post id."),
                    Content = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: false, comment: "Post content."),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Post created at."),
                    ThreadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Thread id."),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Owner id.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_Threads_ThreadId",
                        column: x => x.ThreadId,
                        principalTable: "Threads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Post");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_OwnerId",
                table: "Posts",
                column: "OwnerId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Threads");

            migrationBuilder.DropTable(
                name: "Forums");
        }
    }
}
