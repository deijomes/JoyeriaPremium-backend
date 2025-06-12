using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoyeriaPremiun.Migrations
{
    /// <inheritdoc />
    public partial class direccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroTelefono",
                table: "direcciones");

            migrationBuilder.DropColumn(
                name: "PedidoId",
                table: "direcciones");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NumeroTelefono",
                table: "direcciones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PedidoId",
                table: "direcciones",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
