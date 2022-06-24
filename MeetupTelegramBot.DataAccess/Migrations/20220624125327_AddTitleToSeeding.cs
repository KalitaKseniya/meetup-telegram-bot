using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetupTelegramBot.DataAccess.Migrations
{
    public partial class AddTitleToSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Presentations",
                keyColumn: "Id",
                keyValue: new Guid("9ef9eade-92a2-4277-94df-45b802157ef3"),
                column: "Title",
                value: "Title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Presentations",
                keyColumn: "Id",
                keyValue: new Guid("9ef9eade-92a2-4277-94df-45b802157ef3"),
                column: "Title",
                value: null);
        }
    }
}
