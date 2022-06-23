using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetupTelegramBot.DataAccess.Migrations
{
    public partial class AddSpeackerFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Presentations_SpeackerId",
                table: "Presentations",
                column: "SpeackerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Presentations_Speackers_SpeackerId",
                table: "Presentations",
                column: "SpeackerId",
                principalTable: "Speackers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Presentations_Speackers_SpeackerId",
                table: "Presentations");

            migrationBuilder.DropIndex(
                name: "IX_Presentations_SpeackerId",
                table: "Presentations");
        }
    }
}
