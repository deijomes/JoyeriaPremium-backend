using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoyeriaPremiun.Migrations
{
    /// <inheritdoc />
    public partial class productos5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "precioDeVenta",
                table: "Productos",
                newName: "descuento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "descuento",
                table: "Productos",
                newName: "precioDeVenta");
        }
    }
}
