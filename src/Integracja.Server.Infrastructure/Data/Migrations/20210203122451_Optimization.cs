using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Integracja.Server.Infrastructure.Data.Migrations
{
    public partial class Optimization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswersCount",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "PlayersCount",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Gamemodes");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Questions",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Questions",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "CorrectAnswersCount",
                table: "Questions",
                newName: "RowVersion");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Games",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "QuestionsCount",
                table: "Games",
                newName: "RowVersion");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Games",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Gamemodes",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Gamemodes",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Categories",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "QuestionsCount",
                table: "Categories",
                newName: "RowVersion");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Categories",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Answers",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Answers",
                newName: "CreatedOn");

            migrationBuilder.AddColumn<int>(
                name: "RowVersion",
                table: "Gamemodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RowVersion",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Gamemodes");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Questions",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "RowVersion",
                table: "Questions",
                newName: "CorrectAnswersCount");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Questions",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Games",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "RowVersion",
                table: "Games",
                newName: "QuestionsCount");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Games",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Gamemodes",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Gamemodes",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Categories",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "RowVersion",
                table: "Categories",
                newName: "QuestionsCount");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Categories",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Answers",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Answers",
                newName: "CreatedDate");

            migrationBuilder.AddColumn<int>(
                name: "AnswersCount",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Questions",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlayersCount",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Games",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Gamemodes",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Categories",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Answers",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }
    }
}
