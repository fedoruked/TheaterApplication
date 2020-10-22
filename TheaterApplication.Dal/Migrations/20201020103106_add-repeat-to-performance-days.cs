using Microsoft.EntityFrameworkCore.Migrations;

namespace TheaterApplication.Dal.Migrations
{
    public partial class addrepeattoperformancedays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_repeat_weekly",
                table: "performances_dates",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_repeat_weekly",
                table: "performances_dates");
        }
    }
}
