using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TheaterApplication.Dal.Migrations
{
    public partial class perfomancestables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "performances",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created = table.Column<DateTime>(nullable: false),
                    updated = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    tickets_count = table.Column<int>(nullable: false),
                    tickets_booked = table.Column<int>(nullable: false),
                    duration_minutes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_performances", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "performances_dates",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created = table.Column<DateTime>(nullable: false),
                    updated = table.Column<DateTime>(nullable: false),
                    start_at = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    performance_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_performances_dates", x => x.id);
                    table.ForeignKey(
                        name: "fk_performances_dates_performances_performance_id",
                        column: x => x.performance_id,
                        principalTable: "performances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_performances_dates_performance_id",
                table: "performances_dates",
                column: "performance_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "performances_dates");

            migrationBuilder.DropTable(
                name: "performances");
        }
    }
}
