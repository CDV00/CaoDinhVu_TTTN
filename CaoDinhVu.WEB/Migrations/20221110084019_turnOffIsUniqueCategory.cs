using Microsoft.EntityFrameworkCore.Migrations;

namespace CaoDinhVu.WEB.Migrations
{
    public partial class turnOffIsUniqueCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Categories_Orders",
                table: "Categories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Categories_Orders",
                table: "Categories",
                column: "Orders",
                unique: true,
                filter: "[Orders] IS NOT NULL");
        }
    }
}
