using Microsoft.EntityFrameworkCore.Migrations;

namespace TheaterApplication.Dal.Migrations
{
    public partial class addindexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_performance_posters_schedule_id",
                table: "performance_posters");

            migrationBuilder.CreateIndex(
                name: "ix_performances_name",
                table: "performances",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_performance_schedules_start_at",
                table: "performance_schedules",
                column: "start_at");

            migrationBuilder.CreateIndex(
                name: "ix_performance_posters_schedule_id_difference_from_start_days",
                table: "performance_posters",
                columns: new[] { "schedule_id", "difference_from_start_days" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_performances_name",
                table: "performances");

            migrationBuilder.DropIndex(
                name: "ix_performance_schedules_start_at",
                table: "performance_schedules");

            migrationBuilder.DropIndex(
                name: "ix_performance_posters_schedule_id_difference_from_start_days",
                table: "performance_posters");

            migrationBuilder.CreateIndex(
                name: "ix_performance_posters_schedule_id",
                table: "performance_posters",
                column: "schedule_id");
        }
    }
}
