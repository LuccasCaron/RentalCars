using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCars.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class ajuste_entidade_rental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "initDate",
                table: "Rental",
                newName: "rentalStartDate");

            migrationBuilder.RenameColumn(
                name: "finalDate",
                table: "Rental",
                newName: "rentalEndDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "rentalStartDate",
                table: "Rental",
                newName: "initDate");

            migrationBuilder.RenameColumn(
                name: "rentalEndDate",
                table: "Rental",
                newName: "finalDate");
        }
    }
}
