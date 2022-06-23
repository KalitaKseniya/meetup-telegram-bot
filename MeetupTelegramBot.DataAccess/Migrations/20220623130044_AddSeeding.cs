using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetupTelegramBot.DataAccess.Migrations
{
    public partial class AddSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Meetups",
                columns: new[] { "Id", "Date", "Place", "Time" },
                values: new object[] { new Guid("7ef9eade-92a2-4277-94df-45b802157ef3"), new DateTime(2022, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Polotsk", new TimeSpan(0, 18, 30, 0, 0) });

            migrationBuilder.InsertData(
                table: "Speackers",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[] { new Guid("2ef9eade-92a2-4277-94df-45b802157ef3"), "Ivan", "Ivanov" });

            migrationBuilder.InsertData(
                table: "Feedbacks",
                columns: new[] { "Id", "AuthorName", "FutureProposal", "GeneralFeedback", "MeetupId", "Time" },
                values: new object[] { new Guid("1ef9eade-92a2-4277-94df-45b802157ef3"), "Author Name", "Future proposal", "General Feedback", new Guid("7ef9eade-92a2-4277-94df-45b802157ef3"), new TimeSpan(0, 18, 40, 0, 0) });

            migrationBuilder.InsertData(
                table: "Presentations",
                columns: new[] { "Id", "Description", "SpeackerId", "Title" },
                values: new object[] { new Guid("9ef9eade-92a2-4277-94df-45b802157ef3"), "Description", new Guid("2ef9eade-92a2-4277-94df-45b802157ef3"), null });

            migrationBuilder.InsertData(
                table: "MeetupPresentations",
                columns: new[] { "Id", "MeetupId", "PresentationId" },
                values: new object[] { new Guid("6ef9eade-92a2-4277-94df-45b802157ef3"), new Guid("7ef9eade-92a2-4277-94df-45b802157ef3"), new Guid("9ef9eade-92a2-4277-94df-45b802157ef3") });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "AuthorName", "MeetupPresentationId", "Text", "Time" },
                values: new object[] { new Guid("8ef9eade-92a2-4277-94df-45b802157ef3"), "Author", new Guid("6ef9eade-92a2-4277-94df-45b802157ef3"), "Sample text", new TimeSpan(0, 18, 40, 0, 0) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: new Guid("1ef9eade-92a2-4277-94df-45b802157ef3"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("8ef9eade-92a2-4277-94df-45b802157ef3"));

            migrationBuilder.DeleteData(
                table: "MeetupPresentations",
                keyColumn: "Id",
                keyValue: new Guid("6ef9eade-92a2-4277-94df-45b802157ef3"));

            migrationBuilder.DeleteData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: new Guid("7ef9eade-92a2-4277-94df-45b802157ef3"));

            migrationBuilder.DeleteData(
                table: "Presentations",
                keyColumn: "Id",
                keyValue: new Guid("9ef9eade-92a2-4277-94df-45b802157ef3"));

            migrationBuilder.DeleteData(
                table: "Speackers",
                keyColumn: "Id",
                keyValue: new Guid("2ef9eade-92a2-4277-94df-45b802157ef3"));
        }
    }
}
