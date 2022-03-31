using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace meetup_telegram_bot.Migrations
{
    public partial class AddGeneralQ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Presentations",
                keyColumn: "Id",
                keyValue: new Guid("794db43d-d7a9-44bb-99aa-57e4de7ee59b"));

            migrationBuilder.DeleteData(
                table: "Presentations",
                keyColumn: "Id",
                keyValue: new Guid("8f4f245d-bbfc-4ace-bcf4-51d2cb70ed6d"));

            migrationBuilder.DeleteData(
                table: "Presentations",
                keyColumn: "Id",
                keyValue: new Guid("f3c868d7-3153-468f-be7c-11bff5ee6692"));

            migrationBuilder.InsertData(
                table: "Presentations",
                columns: new[] { "Id", "Description", "SpeackerName", "Title" },
                values: new object[,]
                {
                    { new Guid("03f626a6-3d04-4ffd-8dbb-f0ba307830c5"), "Описание", "Kseniya", "In-live разработка" },
                    { new Guid("46ff2291-6111-4dbf-a3e9-23d2c9e1ad7d"), "Описание", "Сюрприз", "Третий доклад" }
                });

            migrationBuilder.InsertData(
                table: "Presentations",
                columns: new[] { "Id", "Description", "IsDisplayed", "SpeackerName", "Title" },
                values: new object[] { new Guid("958ae825-56f4-4390-90e3-4aa9741673a3"), "Описание", true, "", "Вопрос не по темам" });

            migrationBuilder.InsertData(
                table: "Presentations",
                columns: new[] { "Id", "Description", "SpeackerName", "Title" },
                values: new object[] { new Guid("ddddcfc4-9a9d-4277-9b34-23c195468113"), "Описание", "Hanna", "Путь разработчика" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Presentations",
                keyColumn: "Id",
                keyValue: new Guid("03f626a6-3d04-4ffd-8dbb-f0ba307830c5"));

            migrationBuilder.DeleteData(
                table: "Presentations",
                keyColumn: "Id",
                keyValue: new Guid("46ff2291-6111-4dbf-a3e9-23d2c9e1ad7d"));

            migrationBuilder.DeleteData(
                table: "Presentations",
                keyColumn: "Id",
                keyValue: new Guid("958ae825-56f4-4390-90e3-4aa9741673a3"));

            migrationBuilder.DeleteData(
                table: "Presentations",
                keyColumn: "Id",
                keyValue: new Guid("ddddcfc4-9a9d-4277-9b34-23c195468113"));

            migrationBuilder.InsertData(
                table: "Presentations",
                columns: new[] { "Id", "Description", "IsDisplayed", "SpeackerName", "Title" },
                values: new object[] { new Guid("794db43d-d7a9-44bb-99aa-57e4de7ee59b"), "Описание", false, "Kseniya", "In-live разработка" });

            migrationBuilder.InsertData(
                table: "Presentations",
                columns: new[] { "Id", "Description", "IsDisplayed", "SpeackerName", "Title" },
                values: new object[] { new Guid("8f4f245d-bbfc-4ace-bcf4-51d2cb70ed6d"), "Описание", false, "Сюрприз", "Третий доклад" });

            migrationBuilder.InsertData(
                table: "Presentations",
                columns: new[] { "Id", "Description", "IsDisplayed", "SpeackerName", "Title" },
                values: new object[] { new Guid("f3c868d7-3153-468f-be7c-11bff5ee6692"), "Описание", false, "Hanna", "Путь разработчика" });
        }
    }
}
