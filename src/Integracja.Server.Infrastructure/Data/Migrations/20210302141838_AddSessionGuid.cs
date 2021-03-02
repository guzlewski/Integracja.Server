using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Integracja.Server.Infrastructure.Data.Migrations
{
    public partial class AddSessionGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SessionGuid",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionGuid",
                table: "AspNetUsers");
        }
    }
}
