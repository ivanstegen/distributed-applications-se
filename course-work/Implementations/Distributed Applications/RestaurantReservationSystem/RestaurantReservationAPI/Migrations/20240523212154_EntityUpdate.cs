using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservationAPI.Migrations
{
    /// <inheritdoc />
    public partial class EntityUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ReservationId",
                table: "Users",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Reservations_ReservationId",
                table: "Users",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Reservations_ReservationId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ReservationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Users");
        }
    }
}
