using System.ComponentModel.DataAnnotations;

namespace JoyeriaPremiun.Entidades
{
    public class CompraProductoS
    {
        public int Id { get; set; }

        public int ProductoId { get; set; }  
        public  Producto? Producto { get; set; }
 
        public decimal PrecioDeCompra { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }

        public Compra? Compra { get; set; }
    }

}
