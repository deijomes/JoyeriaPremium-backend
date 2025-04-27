using System.ComponentModel.DataAnnotations;

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

        public decimal precioDeVenta {  get; set; }
       
        public int Stock { get; set; }

        public List<ImagenProducto>? imagenProductos { get; set; } = new List<ImagenProducto>();
        public List<CompraProductoS>? CompraProductos { get; set; } = new List<CompraProductoS>();
        public ProductoDescuento? productoDescuento { get; set; }
    }

}
