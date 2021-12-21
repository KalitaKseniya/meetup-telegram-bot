using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace meetup_telegram_bot.Migrations
{
    public partial class AddPresentationData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Presentations",
                columns: new[] { "Id", "Description", "SpeackerName", "Title" },
                values: new object[] { new Guid("3a8bc096-dff2-4e31-b45a-010a47322836"), "Описание", "Сюрприз", "Третий доклад" });

            migrationBuilder.InsertData(
                table: "Presentations",
                columns: new[] { "Id", "Description", "SpeackerName", "Title" },
                values: new object[] { new Guid("dacb7cdf-ad5a-4cd1-83d4-a02678fd1313"), "Описание", "Kseniya", "In-live разработка" });

            migrationBuilder.InsertData(
                table: "Presentations",
                columns: new[] { "Id", "Description", "SpeackerName", "Title" },
                values: new object[] { new Guid("f7cd069c-b314-45e3-9589-7796e45e5e01"), "Описание", "Hanna", "Путь разработчика" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Presentations",
                keyColumn: "Id",
                keyValue: new Guid("3a8bc096-dff2-4e31-b45a-010a47322836"));

            migrationBuilder.DeleteData(
                table: "Presentations",
                keyColumn: "Id",
                keyValue: new Guid("dacb7cdf-ad5a-4cd1-83d4-a02678fd1313"));

            migrationBuilder.DeleteData(
                table: "Presentations",
                keyColumn: "Id",
                keyValue: new Guid("f7cd069c-b314-45e3-9589-7796e45e5e01"));
        }
    }
}
