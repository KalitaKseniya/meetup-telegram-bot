using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetupTelegramBot.DataAccess.Migrations
{
    public partial class AddFksFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MeetupPresentationId",
                table: "Questions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MeetupId",
                table: "MeetupPresentations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PresentationId",
                table: "MeetupPresentations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MeetupId",
                table: "Feedbacks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_MeetupPresentations_MeetupId_PresentationId",
                table: "MeetupPresentations",
                columns: new[] { "MeetupId", "PresentationId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_MeetupPresentations_MeetupId_PresentationId",
                table: "MeetupPresentations");

            migrationBuilder.DropColumn(
                name: "MeetupPresentationId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "MeetupId",
                table: "MeetupPresentations");

            migrationBuilder.DropColumn(
                name: "PresentationId",
                table: "MeetupPresentations");

            migrationBuilder.DropColumn(
                name: "MeetupId",
                table: "Feedbacks");
        }
    }
}
