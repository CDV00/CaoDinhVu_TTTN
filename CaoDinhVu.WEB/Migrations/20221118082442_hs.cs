using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CaoDinhVu.WEB.Migrations
{
    public partial class hs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOptions_Options_OptionId",
                table: "ProductOptions");

            migrationBuilder.AlterColumn<Guid>(
                name: "OptionId",
                table: "ProductOptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptions_Options_OptionId",
                table: "ProductOptions",
                column: "OptionId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOptions_Options_OptionId",
                table: "ProductOptions");

            migrationBuilder.AlterColumn<Guid>(
                name: "OptionId",
                table: "ProductOptions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptions_Options_OptionId",
                table: "ProductOptions",
                column: "OptionId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
