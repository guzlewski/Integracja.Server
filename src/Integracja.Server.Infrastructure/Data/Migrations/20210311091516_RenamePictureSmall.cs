using Microsoft.EntityFrameworkCore.Migrations;

namespace Integracja.Server.Infrastructure.Data.Migrations
{
    public partial class RenamePictureSmall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PictureSmall",
                table: "AspNetUsers",
                newName: "Thumbnail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Thumbnail",
                table: "AspNetUsers",
                newName: "PictureSmall");
        }
    }
}
