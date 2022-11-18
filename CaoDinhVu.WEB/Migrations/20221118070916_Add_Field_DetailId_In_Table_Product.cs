using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CaoDinhVu.WEB.Migrations
{
    public partial class Add_Field_DetailId_In_Table_Product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Details_Products_ProductId",
                table: "Details");

            migrationBuilder.DropIndex(
                name: "IX_Details_ProductId",
                table: "Details");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Details");

            migrationBuilder.AddColumn<Guid>(
                name: "DetailId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_DetailId",
                table: "Products",
                column: "DetailId",
                unique: true,
                filter: "[DetailId] IS NOT NULL");

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

            migrationBuilder.DropIndex(
                name: "IX_Products_DetailId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DetailId",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Details",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Details_ProductId",
                table: "Details",
                column: "ProductId",
                unique: true,
                filter: "[ProductId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Details_Products_ProductId",
                table: "Details",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
