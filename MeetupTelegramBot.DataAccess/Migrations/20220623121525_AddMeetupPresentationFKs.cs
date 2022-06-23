using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetupTelegramBot.DataAccess.Migrations
{
    public partial class AddMeetupPresentationFKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MeetupPresentations_PresentationId",
                table: "MeetupPresentations",
                column: "PresentationId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetupPresentations_Meetups_MeetupId",
                table: "MeetupPresentations",
                column: "MeetupId",
                principalTable: "Meetups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetupPresentations_Presentations_PresentationId",
                table: "MeetupPresentations",
                column: "PresentationId",
                principalTable: "Presentations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetupPresentations_Meetups_MeetupId",
                table: "MeetupPresentations");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetupPresentations_Presentations_PresentationId",
                table: "MeetupPresentations");

            migrationBuilder.DropIndex(
                name: "IX_MeetupPresentations_PresentationId",
                table: "MeetupPresentations");
        }
    }
}
