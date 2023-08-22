using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCarService.Migrations
{
    public partial class RemovingOnlyTimeBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HourGetCar",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "HourReturnCar",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "HourGetCar",
                table: "Books",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "HourReturnCar",
                table: "Books",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
