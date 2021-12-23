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
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    FirstInteractionAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ExternalIdentifier = table.Column<long>(type: "INTEGER", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    FirstInteractionAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ExternalIdentifier = table.Column<long>(type: "INTEGER", nullable: true),
                    Username = table.Column<string>(type: "TEXT", nullable: true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    LanguageCode = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conversation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    ChatId = table.Column<Guid>(type: "TEXT", nullable: true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: true),
                    FirstInteractionAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    TelegramConversation_UserId = table.Column<Guid>(type: "TEXT", nullable: true),
                    TelegramConversation_ChatId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversation_Chat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Conversation_Chat_TelegramConversation_ChatId",
                        column: x => x.TelegramConversation_ChatId,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Conversation_User_TelegramConversation_UserId",
                        column: x => x.TelegramConversation_UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Conversation_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
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
                    ChatProfile_DataId = table.Column<Guid>(type: "TEXT", nullable: true),
                    UniversalChatId = table.Column<Guid>(type: "TEXT", nullable: true),
                    DataId = table.Column<Guid>(type: "TEXT", nullable: true),
                    UniversalConversationId = table.Column<Guid>(type: "TEXT", nullable: true),
                    UserProfile_DataId = table.Column<Guid>(type: "TEXT", nullable: true),
                    UniversalUserId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_Chat_ChatProfile_DataId",
                        column: x => x.ChatProfile_DataId,
                        principalTable: "Chat",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Profiles_Chat_UniversalChatId",
                        column: x => x.UniversalChatId,
                        principalTable: "Chat",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Profiles_Conversation_DataId",
                        column: x => x.DataId,
                        principalTable: "Conversation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Profiles_Conversation_UniversalConversationId",
                        column: x => x.UniversalConversationId,
                        principalTable: "Conversation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Profiles_User_UniversalUserId",
                        column: x => x.UniversalUserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Profiles_User_UserProfile_DataId",
                        column: x => x.UserProfile_DataId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_ChatId",
                table: "Conversation",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_TelegramConversation_ChatId",
                table: "Conversation",
                column: "TelegramConversation_ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_TelegramConversation_UserId",
                table: "Conversation",
                column: "TelegramConversation_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_UserId",
                table: "Conversation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_ChatProfile_DataId",
                table: "Profiles",
                column: "ChatProfile_DataId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_DataId",
                table: "Profiles",
                column: "DataId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UniversalChatId",
                table: "Profiles",
                column: "UniversalChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UniversalConversationId",
                table: "Profiles",
                column: "UniversalConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UniversalUserId",
                table: "Profiles",
                column: "UniversalUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserProfile_DataId",
                table: "Profiles",
                column: "UserProfile_DataId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Conversation");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
