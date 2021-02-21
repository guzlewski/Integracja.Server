using Microsoft.EntityFrameworkCore.Migrations;

namespace Integracja.Server.Infrastructure.Data.Migrations
{
    public partial class AddColumnsToGameUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnsweredQuestions",
                table: "GameUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorrectlyAnsweredQuestions",
                table: "GameUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IncorrectlyAnsweredQuestions",
                table: "GameUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnsweredQuestions",
                table: "GameUsers");

            migrationBuilder.DropColumn(
                name: "CorrectlyAnsweredQuestions",
                table: "GameUsers");

            migrationBuilder.DropColumn(
                name: "IncorrectlyAnsweredQuestions",
                table: "GameUsers");
        }
    }
}
