using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservationAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEntitiesThird : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationStatus",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "Tables",
                newName: "IsPopular");

            migrationBuilder.AddColumn<int>(
                name: "VipGuests",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VipGuests",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "IsPopular",
                table: "Tables",
                newName: "IsAvailable");

            migrationBuilder.AddColumn<string>(
                name: "ReservationStatus",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
