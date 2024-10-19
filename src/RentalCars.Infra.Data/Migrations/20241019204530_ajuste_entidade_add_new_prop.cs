using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCars.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class ajuste_entidade_add_new_prop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isCompleted",
                table: "Rental",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isCompleted",
                table: "Rental");
        }
    }
}
