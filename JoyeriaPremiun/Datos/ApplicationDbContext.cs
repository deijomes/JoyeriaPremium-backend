using JoyeriaPremiun.Entidades;
using Microsoft.EntityFrameworkCore;

namespace JoyeriaPremiun.Datos
{
    public class ApplicationDbContext : DbContext
    {
        

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Producto>()
             .Property(p => p.Precio)
             .HasPrecision(18, 2);

            modelBuilder.Entity<Producto>()
            .Property(p => p.descuento)
            .HasPrecision(18, 2);


            modelBuilder.Entity<CompraProductoS>()
           .Property(p => p.PrecioDeCompra)
           .HasPrecision(18, 2);

            modelBuilder.Entity<CompraProductoS>()
            .Property(p => p.Total)
            .HasPrecision(18, 2);

            modelBuilder.Entity<ProductoDescuento>()
           .Property(p => p.Descuento)
           .HasPrecision(18, 2);

            modelBuilder.Entity<Venta>()
           .Property(p => p.total)
           .HasPrecision(18, 2);

        }
        public DbSet<CompraProductoS> compraProductos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<ImagenProducto> imagens { get; set; }
        public DbSet<Usuario> UsuarioS { get; set; }
        public DbSet<Compra> compras { get; set; }
        public DbSet<ProductoDescuento> ProductoDescuentos { get;  set; }
        public DbSet<FavoritoProducto> favoritos { get; set; }
        public DbSet<Venta> ventas { get; set; }
        public DbSet<VentaProducto> ventaProductos { get; set; }



    }
}
