using Microsoft.EntityFrameworkCore.Migrations;

namespace TheaterApplication.Dal.Migrations
{
    public partial class generateposters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var reader = new ScriptReader();
            var script = reader.ReadScript("GeneratePostersFunction");
            migrationBuilder.Sql(script);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
