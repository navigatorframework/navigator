using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Navigator.Samples.Store.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: false),
                    FirstInteractionAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: false),
                    FirstInteractionAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ChatId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: false),
                    FirstInteractionAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversations_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Conversations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_ChatId",
                table: "Conversations",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_UserId",
                table: "Conversations",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
