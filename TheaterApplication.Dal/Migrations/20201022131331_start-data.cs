using Microsoft.EntityFrameworkCore.Migrations;

namespace TheaterApplication.Dal.Migrations
{
    public partial class startdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var reader = new ScriptReader();
            var script = reader.ReadScript("StartData");
            migrationBuilder.Sql(script);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
