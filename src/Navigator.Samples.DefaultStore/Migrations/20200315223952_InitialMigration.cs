using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Navigator.Samples.DefaultStore.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NavigatorChats",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NavigatorChats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NavigatorUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsBot = table.Column<bool>(nullable: false),
                    LanguageCode = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NavigatorUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NavigatorConversations",
                columns: table => new
                {
                    ChatId = table.Column<long>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NavigatorConversations", x => new { x.ChatId, x.UserId });
                    table.ForeignKey(
                        name: "FK_NavigatorConversations_NavigatorChats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "NavigatorChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NavigatorConversations_NavigatorUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "NavigatorUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NavigatorConversations_UserId",
                table: "NavigatorConversations",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NavigatorConversations");

            migrationBuilder.DropTable(
                name: "NavigatorChats");

            migrationBuilder.DropTable(
                name: "NavigatorUsers");
        }
    }
}
