using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Integracja.Server.Infrastructure.Data.Migrations
{
    public partial class Refactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "GameUsers",
                newName: "GameUserState");

            migrationBuilder.AlterColumn<int>(
                name: "MaxPlayersCount",
                table: "Games",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TimeForFullQuiz",
                table: "Gamemodes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GameUserState",
                table: "GameUsers",
                newName: "State");

            migrationBuilder.AlterColumn<int>(
                name: "MaxPlayersCount",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TimeForFullQuiz",
                table: "Gamemodes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedOn",
                table: "Answers",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RowVersion",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedOn",
                table: "Answers",
                type: "datetimeoffset",
                nullable: true);
        }
    }
}
