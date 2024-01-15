using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RekdApi.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUserGSRef : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameSessionUser");

            migrationBuilder.AddColumn<Guid>(
                name: "GameSessionId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GameSessionId",
                table: "AspNetUsers",
                column: "GameSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_GameSessions_GameSessionId",
                table: "AspNetUsers",
                column: "GameSessionId",
                principalTable: "GameSessions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_GameSessions_GameSessionId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GameSessionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GameSessionId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "GameSessionUser",
                columns: table => new
                {
                    GameSessionsId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayersId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSessionUser", x => new { x.GameSessionsId, x.PlayersId });
                    table.ForeignKey(
                        name: "FK_GameSessionUser_AspNetUsers_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameSessionUser_GameSessions_GameSessionsId",
                        column: x => x.GameSessionsId,
                        principalTable: "GameSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameSessionUser_PlayersId",
                table: "GameSessionUser",
                column: "PlayersId");
        }
    }
}
