using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoyeriaPremiun.Entidades
{
    public class Producto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Codigo { get; set; }
        public string? Categoria { get; set; }
        public string? Descripcion { get; set; }  
        public decimal Precio{ get; set; }

        public decimal descuento {  get; set; }
       
        public int Stock { get; set; }

        public List<ImagenProducto>? imagenProductos { get; set; } = new List<ImagenProducto>();
        public List<CompraProductoS>? CompraProductos { get; set; } = new List<CompraProductoS>();
       
        public ProductoDescuento? productoDescuento { get; set; }
        [NotMapped]
        public decimal PrecioDeVenta
        {
            get
            {
                return Precio * (1 - descuento / 100m);
            }
        }

        public List<FavoritoProducto>? Favoritos { get; set; } = new();
    }

}
