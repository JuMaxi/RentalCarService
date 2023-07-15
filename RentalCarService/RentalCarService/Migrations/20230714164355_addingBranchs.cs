using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCarService.Migrations
{
    public partial class addingBranchs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpeningHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Opens = table.Column<TimeSpan>(type: "time", nullable: false),
                    Closes = table.Column<TimeSpan>(type: "time", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    BranchsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpeningHours_Branches_BranchsId",
                        column: x => x.BranchsId,
                        principalTable: "Branches",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHours_BranchsId",
                table: "OpeningHours",
                column: "BranchsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpeningHours");

            migrationBuilder.DropTable(
                name: "Branches");
        }
    }
}
