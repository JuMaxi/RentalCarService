using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCarService.Migrations
{
    public partial class RemovingCars : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Brands_BrandId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_Categories_CategoryId",
                table: "Car");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Car",
                table: "Car");

            migrationBuilder.RenameTable(
                name: "Car",
                newName: "Fleet");

            migrationBuilder.RenameIndex(
                name: "IX_Car_CategoryId",
                table: "Fleet",
                newName: "IX_Fleet_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Car_BrandId",
                table: "Fleet",
                newName: "IX_Fleet_BrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fleet",
                table: "Fleet",
                column: "Id");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fleet",
                table: "Fleet");

            migrationBuilder.RenameTable(
                name: "Fleet",
                newName: "Car");

            migrationBuilder.RenameIndex(
                name: "IX_Fleet_CategoryId",
                table: "Car",
                newName: "IX_Car_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Fleet_BrandId",
                table: "Car",
                newName: "IX_Car_BrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Car",
                table: "Car",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Brands_BrandId",
                table: "Car",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Categories_CategoryId",
                table: "Car",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
