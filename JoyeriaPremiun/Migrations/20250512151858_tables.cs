using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoyeriaPremiun.Migrations
{
    /// <inheritdoc />
    public partial class tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "compras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    proveedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaDeCompra = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_compras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    descuento = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "compraProductos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    PrecioDeCompra = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CompraId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_compraProductos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_compraProductos_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_compraProductos_compras_CompraId",
                        column: x => x.CompraId,
                        principalTable: "compras",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "imagens",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    foto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_imagens", x => x.id);
                    table.ForeignKey(
                        name: "FK_imagens_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductoDescuentos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoDescuentos", x => x.id);
                    table.ForeignKey(
                        name: "FK_ProductoDescuentos_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "direcciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Calle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Carrera = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroTelefono = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_direcciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_direcciones_UsuarioS_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "UsuarioS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "favoritos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favoritos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_favoritos_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_favoritos_UsuarioS_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "UsuarioS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ventas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaDeCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ventas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ventas_UsuarioS_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "UsuarioS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ventaProductos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    VentaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ventaProductos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ventaProductos_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ventaProductos_ventas_VentaId",
                        column: x => x.VentaId,
                        principalTable: "ventas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_compraProductos_CompraId",
                table: "compraProductos",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_compraProductos_ProductoId",
                table: "compraProductos",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_direcciones_UsuarioId",
                table: "direcciones",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_favoritos_ProductoId",
                table: "favoritos",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_favoritos_UsuarioId",
                table: "favoritos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_imagens_ProductoId",
                table: "imagens",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoDescuentos_ProductoId",
                table: "ProductoDescuentos",
                column: "ProductoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ventaProductos_ProductoId",
                table: "ventaProductos",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_ventaProductos_VentaId",
                table: "ventaProductos",
                column: "VentaId");

            migrationBuilder.CreateIndex(
                name: "IX_ventas_usuarioId",
                table: "ventas",
                column: "usuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "compraProductos");

            migrationBuilder.DropTable(
                name: "direcciones");

            migrationBuilder.DropTable(
                name: "favoritos");

            migrationBuilder.DropTable(
                name: "imagens");

            migrationBuilder.DropTable(
                name: "ProductoDescuentos");

            migrationBuilder.DropTable(
                name: "ventaProductos");

            migrationBuilder.DropTable(
                name: "compras");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "ventas");

            migrationBuilder.DropTable(
                name: "UsuarioS");
        }
    }
}
