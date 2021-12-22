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
                    ChatId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstInteractionAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => new { x.ChatId, x.UserId });
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

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Provider = table.Column<string>(type: "TEXT", nullable: false),
                    Identification = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    UniversalChatId = table.Column<Guid>(type: "TEXT", nullable: true),
                    UniversalConversationChatId = table.Column<Guid>(type: "TEXT", nullable: true),
                    UniversalConversationUserId = table.Column<Guid>(type: "TEXT", nullable: true),
                    UniversalUserId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_Chats_UniversalChatId",
                        column: x => x.UniversalChatId,
                        principalTable: "Chats",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Profiles_Conversations_UniversalConversationChatId_UniversalConversationUserId",
                        columns: x => new { x.UniversalConversationChatId, x.UniversalConversationUserId },
                        principalTable: "Conversations",
                        principalColumns: new[] { "ChatId", "UserId" });
                    table.ForeignKey(
                        name: "FK_Profiles_Users_UniversalUserId",
                        column: x => x.UniversalUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_UserId",
                table: "Conversations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UniversalChatId",
                table: "Profiles",
                column: "UniversalChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UniversalConversationChatId_UniversalConversationUserId",
                table: "Profiles",
                columns: new[] { "UniversalConversationChatId", "UniversalConversationUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UniversalUserId",
                table: "Profiles",
                column: "UniversalUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
