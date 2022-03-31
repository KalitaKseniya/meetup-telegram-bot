using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace meetup_telegram_bot.Migrations
{
    public partial class AddFlagToShowPresentation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDisplayed",
                table: "Presentations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Presentations",
                columns: new[] { "Id", "Description", "IsDisplayed", "SpeackerName", "Title" },
                values: new object[] { new Guid("0c03ba0b-3b46-42ba-ba39-6b635c9a4bc0"), "Описание", true, "Kseniya", "REST-архитектура или как усидеть на 6 стулья" });

            migrationBuilder.InsertData(
                table: "Presentations",
                columns: new[] { "Id", "Description", "SpeackerName", "Title" },
                values: new object[,]
                {
                    { new Guid("794db43d-d7a9-44bb-99aa-57e4de7ee59b"), "Описание", "Kseniya", "In-live разработка" },
                    { new Guid("8f4f245d-bbfc-4ace-bcf4-51d2cb70ed6d"), "Описание", "Сюрприз", "Третий доклад" }
                });

            migrationBuilder.InsertData(
                table: "Presentations",
                columns: new[] { "Id", "Description", "IsDisplayed", "SpeackerName", "Title" },
                values: new object[] { new Guid("99d09f48-0fec-4ef4-8292-2bab81de8d37"), "Описание", true, "Илья", "Реалии фуллстека" });

            migrationBuilder.InsertData(
                table: "Presentations",
                columns: new[] { "Id", "Description", "SpeackerName", "Title" },
                values: new object[] { new Guid("f3c868d7-3153-468f-be7c-11bff5ee6692"), "Описание", "Hanna", "Путь разработчика" });

            migrationBuilder.InsertData(
                table: "Presentations",
                columns: new[] { "Id", "Description", "IsDisplayed", "SpeackerName", "Title" },
                values: new object[] { new Guid("fadabc27-40e4-47f3-bc1b-f0916b4772cd"), "Описание", true, "Hanna", "Базы данных. То, что вы РЕАЛЬНО будете использовать на проекте" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Presentations",
                keyColumn: "Id",
                keyValue: new Guid("0c03ba0b-3b46-42ba-ba39-6b635c9a4bc0"));

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
                keyValue: new Guid("99d09f48-0fec-4ef4-8292-2bab81de8d37"));

            migrationBuilder.DeleteData(
                table: "Presentations",
                keyColumn: "Id",
                keyValue: new Guid("f3c868d7-3153-468f-be7c-11bff5ee6692"));

            migrationBuilder.DeleteData(
                table: "Presentations",
                keyColumn: "Id",
                keyValue: new Guid("fadabc27-40e4-47f3-bc1b-f0916b4772cd"));

            migrationBuilder.DropColumn(
                name: "IsDisplayed",
                table: "Presentations");
        }
    }
}
