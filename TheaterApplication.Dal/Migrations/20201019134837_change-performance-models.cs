using Microsoft.EntityFrameworkCore.Migrations;

namespace TheaterApplication.Dal.Migrations
{
    public partial class changeperformancemodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_performances_dates_performances_performance_id",
                table: "performances_dates");

            migrationBuilder.DropColumn(
                name: "tickets_booked",
                table: "performances");

            migrationBuilder.DropColumn(
                name: "tickets_count",
                table: "performances");

            migrationBuilder.AlterColumn<int>(
                name: "performance_id",
                table: "performances_dates",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tickets_booked",
                table: "performances_dates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "tickets_count",
                table: "performances_dates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "fk_performances_dates_performances_performance_id",
                table: "performances_dates",
                column: "performance_id",
                principalTable: "performances",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_performances_dates_performances_performance_id",
                table: "performances_dates");

            migrationBuilder.DropColumn(
                name: "tickets_booked",
                table: "performances_dates");

            migrationBuilder.DropColumn(
                name: "tickets_count",
                table: "performances_dates");

            migrationBuilder.AlterColumn<int>(
                name: "performance_id",
                table: "performances_dates",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "tickets_booked",
                table: "performances",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "tickets_count",
                table: "performances",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "fk_performances_dates_performances_performance_id",
                table: "performances_dates",
                column: "performance_id",
                principalTable: "performances",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
