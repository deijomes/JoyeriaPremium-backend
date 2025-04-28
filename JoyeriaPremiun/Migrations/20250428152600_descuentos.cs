using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoyeriaPremiun.Migrations
{
    /// <inheritdoc />
    public partial class descuentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductoDescuento_Productos_ProductoId",
                table: "ProductoDescuento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductoDescuento",
                table: "ProductoDescuento");

            migrationBuilder.RenameTable(
                name: "ProductoDescuento",
                newName: "ProductoDescuentos");

            migrationBuilder.RenameIndex(
                name: "IX_ProductoDescuento_ProductoId",
                table: "ProductoDescuentos",
                newName: "IX_ProductoDescuentos_ProductoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductoDescuentos",
                table: "ProductoDescuentos",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductoDescuentos_Productos_ProductoId",
                table: "ProductoDescuentos",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductoDescuentos_Productos_ProductoId",
                table: "ProductoDescuentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductoDescuentos",
                table: "ProductoDescuentos");

            migrationBuilder.RenameTable(
                name: "ProductoDescuentos",
                newName: "ProductoDescuento");

            migrationBuilder.RenameIndex(
                name: "IX_ProductoDescuentos_ProductoId",
                table: "ProductoDescuento",
                newName: "IX_ProductoDescuento_ProductoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductoDescuento",
                table: "ProductoDescuento",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductoDescuento_Productos_ProductoId",
                table: "ProductoDescuento",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
