using Microsoft.EntityFrameworkCore.Migrations;

namespace Integracja.Server.Infrastructure.Data.Migrations
{
    public partial class ChangeFieldName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_AuthorId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Gamemodes_AspNetUsers_AuthorId",
                table: "Gamemodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_AspNetUsers_AuthorId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_AspNetUsers_AuthorId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Questions",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_AuthorId",
                table: "Questions",
                newName: "IX_Questions_OwnerId");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Games",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Games_AuthorId",
                table: "Games",
                newName: "IX_Games_OwnerId");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Gamemodes",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Gamemodes_AuthorId",
                table: "Gamemodes",
                newName: "IX_Gamemodes_OwnerId");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Categories",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_AuthorId",
                table: "Categories",
                newName: "IX_Categories_OwnerId");

            migrationBuilder.AlterColumn<int>(
                name: "TimeForFullQuiz",
                table: "Gamemodes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_OwnerId",
                table: "Categories",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gamemodes_AspNetUsers_OwnerId",
                table: "Gamemodes",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_AspNetUsers_OwnerId",
                table: "Games",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_AspNetUsers_OwnerId",
                table: "Questions",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_OwnerId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Gamemodes_AspNetUsers_OwnerId",
                table: "Gamemodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_AspNetUsers_OwnerId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_AspNetUsers_OwnerId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Questions",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_OwnerId",
                table: "Questions",
                newName: "IX_Questions_AuthorId");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Games",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Games_OwnerId",
                table: "Games",
                newName: "IX_Games_AuthorId");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Gamemodes",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Gamemodes_OwnerId",
                table: "Gamemodes",
                newName: "IX_Gamemodes_AuthorId");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Categories",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_OwnerId",
                table: "Categories",
                newName: "IX_Categories_AuthorId");

            migrationBuilder.AlterColumn<int>(
                name: "TimeForFullQuiz",
                table: "Gamemodes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_AuthorId",
                table: "Categories",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gamemodes_AspNetUsers_AuthorId",
                table: "Gamemodes",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_AspNetUsers_AuthorId",
                table: "Games",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_AspNetUsers_AuthorId",
                table: "Questions",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
