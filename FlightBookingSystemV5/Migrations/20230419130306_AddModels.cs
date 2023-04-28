using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightBookingSystemV5.Migrations
{
    public partial class AddModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JourneyDetails",
                columns: table => new
                {
                    JourneyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightId = table.Column<string>(type: "VARCHAR(15)", maxLength: 15, nullable: false),
                    AirlineName = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    StartLoc = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: false),
                    EndLoc = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EClassPrice = table.Column<int>(type: "int", nullable: false),
                    BClassPrice = table.Column<int>(type: "int", nullable: false),
                    EClassCapacity = table.Column<int>(type: "int", nullable: false),
                    EClassAvailableSeats = table.Column<int>(type: "int", nullable: false),
                    BClassCapacity = table.Column<int>(type: "int", nullable: false),
                    BClassAvailableSeats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JourneyDetails", x => x.JourneyId);
                });

            migrationBuilder.CreateTable(
                name: "BookingDetails",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    SeatNo = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    UserId = table.Column<string>(type: "NVARCHAR(450)", maxLength: 450, nullable: false),
                    JourneyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDetails", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_BookingDetails_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingDetails_JourneyDetails_JourneyId",
                        column: x => x.JourneyId,
                        principalTable: "JourneyDetails",
                        principalColumn: "JourneyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_JourneyId",
                table: "BookingDetails",
                column: "JourneyId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_UserId",
                table: "BookingDetails",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingDetails");

            migrationBuilder.DropTable(
                name: "JourneyDetails");
        }
    }
}
