using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCarService.Migrations
{
    public partial class CreatingCarBranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarBranchId",
                table: "Fleet",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CarBranch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBranch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarBranch_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fleet_CarBranchId",
                table: "Fleet",
                column: "CarBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CarBranch_BranchId",
                table: "CarBranch",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fleet_CarBranch_CarBranchId",
                table: "Fleet",
                column: "CarBranchId",
                principalTable: "CarBranch",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fleet_CarBranch_CarBranchId",
                table: "Fleet");

            migrationBuilder.DropTable(
                name: "CarBranch");

            migrationBuilder.DropIndex(
                name: "IX_Fleet_CarBranchId",
                table: "Fleet");

            migrationBuilder.DropColumn(
                name: "CarBranchId",
                table: "Fleet");
        }
    }
}
