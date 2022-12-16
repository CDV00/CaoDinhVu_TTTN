using Microsoft.EntityFrameworkCore.Migrations;

namespace CaoDinhVu.WEB.Migrations
{
    public partial class addStaustProductOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ProductOptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ProductColors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "AppUser",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ProductOptions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ProductColors");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AppUser");
        }
    }
}
