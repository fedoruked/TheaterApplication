using Microsoft.EntityFrameworkCore.Migrations;

namespace TheaterApplication.Dal.Migrations
{
    public partial class fixautocreatekey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_performance_posters_performance_schedules_schedule_id",
                table: "performance_posters");

            migrationBuilder.DropColumn(
                name: "performance_schedule_id",
                table: "performance_posters");

            migrationBuilder.AlterColumn<int>(
                name: "schedule_id",
                table: "performance_posters",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_performance_posters_performance_schedules_schedule_id",
                table: "performance_posters",
                column: "schedule_id",
                principalTable: "performance_schedules",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_performance_posters_performance_schedules_schedule_id",
                table: "performance_posters");

            migrationBuilder.AlterColumn<int>(
                name: "schedule_id",
                table: "performance_posters",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "performance_schedule_id",
                table: "performance_posters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "fk_performance_posters_performance_schedules_schedule_id",
                table: "performance_posters",
                column: "schedule_id",
                principalTable: "performance_schedules",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
