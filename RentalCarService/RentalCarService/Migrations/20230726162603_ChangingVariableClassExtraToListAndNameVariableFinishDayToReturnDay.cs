using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCarService.Migrations
{
    public partial class ChangingVariableClassExtraToListAndNameVariableFinishDayToReturnDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Extras_ExtraId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_ExtraId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ExtraId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "FinishDay",
                table: "Books",
                newName: "ReturnDay");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Extras",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Extras_BookId",
                table: "Extras",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Extras_Books_BookId",
                table: "Extras",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Extras_Books_BookId",
                table: "Extras");

            migrationBuilder.DropIndex(
                name: "IX_Extras_BookId",
                table: "Extras");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Extras");

            migrationBuilder.RenameColumn(
                name: "ReturnDay",
                table: "Books",
                newName: "FinishDay");

            migrationBuilder.AddColumn<int>(
                name: "ExtraId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_ExtraId",
                table: "Books",
                column: "ExtraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Extras_ExtraId",
                table: "Books",
                column: "ExtraId",
                principalTable: "Extras",
                principalColumn: "Id");
        }
    }
}
