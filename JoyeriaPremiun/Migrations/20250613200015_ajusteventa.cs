using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoyeriaPremiun.Migrations
{
    /// <inheritdoc />
    public partial class ajusteventa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ventas_AspNetUsers_usuarioId",
                table: "ventas");

            migrationBuilder.RenameColumn(
                name: "usuarioId",
                table: "ventas",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_ventas_usuarioId",
                table: "ventas",
                newName: "IX_ventas_UsuarioId");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Pedidos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_UsuarioId",
                table: "Pedidos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_AspNetUsers_UsuarioId",
                table: "Pedidos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ventas_AspNetUsers_UsuarioId",
                table: "ventas",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_AspNetUsers_UsuarioId",
                table: "Pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_ventas_AspNetUsers_UsuarioId",
                table: "ventas");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_UsuarioId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Pedidos");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "ventas",
                newName: "usuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_ventas_UsuarioId",
                table: "ventas",
                newName: "IX_ventas_usuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_ventas_AspNetUsers_usuarioId",
                table: "ventas",
                column: "usuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
