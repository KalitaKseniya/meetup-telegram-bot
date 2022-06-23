using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetupTelegramBot.DataAccess.Migrations
{
    public partial class AddFeedbackFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_MeetupId",
                table: "Feedbacks",
                column: "MeetupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Meetups_MeetupId",
                table: "Feedbacks",
                column: "MeetupId",
                principalTable: "Meetups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Meetups_MeetupId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_MeetupId",
                table: "Feedbacks");
        }
    }
}
