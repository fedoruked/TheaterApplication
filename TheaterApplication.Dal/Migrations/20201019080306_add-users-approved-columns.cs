using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheaterApplication.Dal.Migrations
{
    public partial class addusersapprovedcolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "approve_code",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "approved",
                table: "users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "approve_code",
                table: "users");

            migrationBuilder.DropColumn(
                name: "approved",
                table: "users");
        }
    }
}
