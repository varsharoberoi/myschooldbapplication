using Microsoft.EntityFrameworkCore.Migrations;

namespace myschooldbapplication.Migrations
{
    public partial class feeyeartext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Feeyeartext",
                table: "FeeType",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Feeyeartext",
                table: "FeeType");
        }
    }
}
