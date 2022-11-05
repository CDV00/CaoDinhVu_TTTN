using Microsoft.EntityFrameworkCore.Migrations;

namespace CaoDinhVu.WEB.Migrations
{
    public partial class ChageAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Detail_DetailId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Detail",
                table: "Detail");

            migrationBuilder.RenameTable(
                name: "Detail",
                newName: "Details");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AppUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AppUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Details",
                table: "Details",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Details_DetailId",
                table: "Products",
                column: "DetailId",
                principalTable: "Details",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Details_DetailId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Details",
                table: "Details");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AppUser");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AppUser");

            migrationBuilder.RenameTable(
                name: "Details",
                newName: "Detail");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Detail",
                table: "Detail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Detail_DetailId",
                table: "Products",
                column: "DetailId",
                principalTable: "Detail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
