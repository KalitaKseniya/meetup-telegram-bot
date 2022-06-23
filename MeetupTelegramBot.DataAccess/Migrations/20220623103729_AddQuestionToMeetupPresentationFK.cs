using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetupTelegramBot.DataAccess.Migrations
{
    public partial class AddQuestionToMeetupPresentationFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Questions_MeetupPresentationId",
                table: "Questions",
                column: "MeetupPresentationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_MeetupPresentations_MeetupPresentationId",
                table: "Questions",
                column: "MeetupPresentationId",
                principalTable: "MeetupPresentations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_MeetupPresentations_MeetupPresentationId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_MeetupPresentationId",
                table: "Questions");
        }
    }
}
