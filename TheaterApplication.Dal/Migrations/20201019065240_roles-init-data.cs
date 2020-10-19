using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheaterApplication.Dal.Migrations
{
    public partial class rolesinitdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "expired",
                table: "tokens",
                nullable: true);

            migrationBuilder.Sql("INSERT INTO roles(id, name, created, updated) " +
                "VALUES (1, 'admin', now() at time zone 'utc', now() at time zone 'utc'), " +
                "(2, 'client', now() at time zone 'utc', now() at time zone 'utc')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "expired",
                table: "tokens");
        }
    }
}
