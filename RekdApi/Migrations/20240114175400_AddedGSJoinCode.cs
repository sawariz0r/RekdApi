using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RekdApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedGSJoinCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "GameSessions");

            migrationBuilder.AddColumn<string>(
                name: "JoinCode",
                table: "GameSessions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JoinCode",
                table: "GameSessions");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "GameSessions",
                type: "text",
                nullable: true);
        }
    }
}
