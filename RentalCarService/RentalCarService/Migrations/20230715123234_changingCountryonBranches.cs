using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCarService.Migrations
{
    public partial class changingCountryonBranches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Branches",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_CountryId",
                table: "Branches",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Countries_CountryId",
                table: "Branches",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Countries_CountryId",
                table: "Branches");

            migrationBuilder.DropIndex(
                name: "IX_Branches_CountryId",
                table: "Branches");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Branches",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
