using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace meetup_telegram_bot.Migrations
{
    public partial class PresentationIdAndAuthorNameRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Presentations_PresentationId",
                table: "Questions");

            migrationBuilder.AlterColumn<Guid>(
                name: "PresentationId",
                table: "Questions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AuthorName",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Presentations_PresentationId",
                table: "Questions",
                column: "PresentationId",
                principalTable: "Presentations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Presentations_PresentationId",
                table: "Questions");

            migrationBuilder.AlterColumn<Guid>(
                name: "PresentationId",
                table: "Questions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorName",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Presentations_PresentationId",
                table: "Questions",
                column: "PresentationId",
                principalTable: "Presentations",
                principalColumn: "Id");
        }
    }
}
