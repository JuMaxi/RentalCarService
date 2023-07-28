using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCarService.Migrations
{
    public partial class CreatingBookExtra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "BookExtra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: true),
                    ExtraId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookExtra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookExtra_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookExtra_Extras_ExtraId",
                        column: x => x.ExtraId,
                        principalTable: "Extras",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookExtra_BookId",
                table: "BookExtra",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookExtra_ExtraId",
                table: "BookExtra",
                column: "ExtraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookExtra");

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
    }
}
