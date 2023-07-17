using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCarService.Migrations
{
    public partial class changingNameProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fleet_Brands_BrandIdId",
                table: "Fleet");

            migrationBuilder.DropForeignKey(
                name: "FK_Fleet_Categories_CategoryIdId",
                table: "Fleet");

            migrationBuilder.RenameColumn(
                name: "CategoryIdId",
                table: "Fleet",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "BrandIdId",
                table: "Fleet",
                newName: "BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_Fleet_CategoryIdId",
                table: "Fleet",
                newName: "IX_Fleet_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Fleet_BrandIdId",
                table: "Fleet",
                newName: "IX_Fleet_BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fleet_Brands_BrandId",
                table: "Fleet",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fleet_Categories_CategoryId",
                table: "Fleet",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fleet_Brands_BrandId",
                table: "Fleet");

            migrationBuilder.DropForeignKey(
                name: "FK_Fleet_Categories_CategoryId",
                table: "Fleet");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Fleet",
                newName: "CategoryIdId");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "Fleet",
                newName: "BrandIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Fleet_CategoryId",
                table: "Fleet",
                newName: "IX_Fleet_CategoryIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Fleet_BrandId",
                table: "Fleet",
                newName: "IX_Fleet_BrandIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fleet_Brands_BrandIdId",
                table: "Fleet",
                column: "BrandIdId",
                principalTable: "Brands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fleet_Categories_CategoryIdId",
                table: "Fleet",
                column: "CategoryIdId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
