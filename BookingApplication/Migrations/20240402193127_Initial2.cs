using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingApplication.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BookReservations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookReservations_UserId",
                table: "BookReservations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookReservations_AspNetUsers_UserId",
                table: "BookReservations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookReservations_AspNetUsers_UserId",
                table: "BookReservations");

            migrationBuilder.DropIndex(
                name: "IX_BookReservations_UserId",
                table: "BookReservations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BookReservations");
        }
    }
}
