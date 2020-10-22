using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TheaterApplication.Dal.Migrations
{
    public partial class performanceschedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "performances_dates");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "performances");

            migrationBuilder.CreateTable(
                name: "performance_schedules",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created = table.Column<DateTime>(nullable: false),
                    updated = table.Column<DateTime>(nullable: false),
                    performance_id = table.Column<int>(nullable: false),
                    start_at = table.Column<DateTime>(nullable: false),
                    tickets_count = table.Column<int>(nullable: false),
                    is_repeat = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_performance_schedules", x => x.id);
                    table.ForeignKey(
                        name: "fk_performance_schedules_performances_performance_id",
                        column: x => x.performance_id,
                        principalTable: "performances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "performance_posters",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created = table.Column<DateTime>(nullable: false),
                    updated = table.Column<DateTime>(nullable: false),
                    performance_schedule_id = table.Column<int>(nullable: false),
                    event_date = table.Column<DateTime>(nullable: false),
                    difference_from_start_days = table.Column<int>(nullable: false),
                    schedule_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_performance_posters", x => x.id);
                    table.ForeignKey(
                        name: "fk_performance_posters_performance_schedules_schedule_id",
                        column: x => x.schedule_id,
                        principalTable: "performance_schedules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "performance_bookings",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created = table.Column<DateTime>(nullable: false),
                    updated = table.Column<DateTime>(nullable: false),
                    poster_id = table.Column<int>(nullable: false),
                    user_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_performance_bookings", x => x.id);
                    table.ForeignKey(
                        name: "fk_performance_bookings_performance_posters_poster_id",
                        column: x => x.poster_id,
                        principalTable: "performance_posters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_performance_bookings_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_performance_bookings_poster_id",
                table: "performance_bookings",
                column: "poster_id");

            migrationBuilder.CreateIndex(
                name: "ix_performance_bookings_user_id",
                table: "performance_bookings",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_performance_posters_schedule_id",
                table: "performance_posters",
                column: "schedule_id");

            migrationBuilder.CreateIndex(
                name: "ix_performance_schedules_performance_id",
                table: "performance_schedules",
                column: "performance_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "performance_bookings");

            migrationBuilder.DropTable(
                name: "performance_posters");

            migrationBuilder.DropTable(
                name: "performance_schedules");

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "performances",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "performances_dates",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    is_repeat_weekly = table.Column<bool>(type: "boolean", nullable: false),
                    performance_id = table.Column<int>(type: "integer", nullable: false),
                    start_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    tickets_booked = table.Column<int>(type: "integer", nullable: false),
                    tickets_count = table.Column<int>(type: "integer", nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_performances_dates", x => x.id);
                    table.ForeignKey(
                        name: "fk_performances_dates_performances_performance_id",
                        column: x => x.performance_id,
                        principalTable: "performances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_performances_dates_performance_id",
                table: "performances_dates",
                column: "performance_id");
        }
    }
}
