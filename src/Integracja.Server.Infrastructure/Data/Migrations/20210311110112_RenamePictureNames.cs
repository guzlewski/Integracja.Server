using Microsoft.EntityFrameworkCore.Migrations;

namespace Integracja.Server.Infrastructure.Data.Migrations
{
    public partial class RenamePictureNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Thumbnail",
                table: "AspNetUsers",
                newName: "ProfileThumbnail");

            migrationBuilder.RenameColumn(
                name: "Picture",
                table: "AspNetUsers",
                newName: "ProfilePicture");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfileThumbnail",
                table: "AspNetUsers",
                newName: "Thumbnail");

            migrationBuilder.RenameColumn(
                name: "ProfilePicture",
                table: "AspNetUsers",
                newName: "Picture");
        }
    }
}
