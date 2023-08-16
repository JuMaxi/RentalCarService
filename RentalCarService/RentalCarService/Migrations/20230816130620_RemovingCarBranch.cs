using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCarService.Migrations
{
    public partial class RemovingCarBranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Fleet",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fleet_BranchId",
                table: "Fleet",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fleet_Branches_BranchId",
                table: "Fleet",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
