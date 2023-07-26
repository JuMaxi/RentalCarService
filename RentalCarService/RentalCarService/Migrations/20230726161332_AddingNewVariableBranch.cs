using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCarService.Migrations
{
    public partial class AddingNewVariableBranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Branches_BranchId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "BranchId",
                table: "Books",
                newName: "BranchReturnId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_BranchId",
                table: "Books",
                newName: "IX_Books_BranchReturnId");

            migrationBuilder.AddColumn<int>(
                name: "BranchGetId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_BranchGetId",
                table: "Books",
                column: "BranchGetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Branches_BranchGetId",
                table: "Books",
                column: "BranchGetId",
                principalTable: "Branches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Branches_BranchReturnId",
                table: "Books",
                column: "BranchReturnId",
                principalTable: "Branches",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Branches_BranchGetId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Branches_BranchReturnId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_BranchGetId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BranchGetId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "BranchReturnId",
                table: "Books",
                newName: "BranchId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_BranchReturnId",
                table: "Books",
                newName: "IX_Books_BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Branches_BranchId",
                table: "Books",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id");
        }
    }
}
