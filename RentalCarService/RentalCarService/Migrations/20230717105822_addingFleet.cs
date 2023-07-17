using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCarService.Migrations
{
    public partial class addingFleet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fleet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandIdId = table.Column<int>(type: "int", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transmission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seats = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirConditioner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrunkSize = table.Column<int>(type: "int", nullable: false),
                    NumberPlate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryIdId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fleet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fleet_Brands_BrandIdId",
                        column: x => x.BrandIdId,
                        principalTable: "Brands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Fleet_Categories_CategoryIdId",
                        column: x => x.CategoryIdId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fleet_BrandIdId",
                table: "Fleet",
                column: "BrandIdId");

            migrationBuilder.CreateIndex(
                name: "IX_Fleet_CategoryIdId",
                table: "Fleet",
                column: "CategoryIdId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fleet");
        }
    }
}
