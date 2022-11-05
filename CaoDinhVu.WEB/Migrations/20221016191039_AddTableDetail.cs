using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CaoDinhVu.WEB.Migrations
{
    public partial class AddTableDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Detail",
                table: "Products",
                newName: "Thumbnails");

            migrationBuilder.AddColumn<Guid>(
                name: "DetailId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ProductOptions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Detail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Screen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Camera = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatingSystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ROM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RAM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Connection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Battery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Charger = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeneralInformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detail", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_DetailId",
                table: "Products",
                column: "DetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Detail_DetailId",
                table: "Products",
                column: "DetailId",
                principalTable: "Detail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Detail_DetailId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Detail");

            migrationBuilder.DropIndex(
                name: "IX_Products_DetailId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DetailId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ProductOptions");

            migrationBuilder.RenameColumn(
                name: "Thumbnails",
                table: "Products",
                newName: "Detail");
        }
    }
}
