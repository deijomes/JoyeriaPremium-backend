using JoyeriaPremiun.Entidades;

namespace JoyeriaPremiun.DTOS
{
    public class compraProductoDTO
    {

        public int ProductoId { get; set; }
        public string? Nombre { get; set; }

        public decimal PrecioDeCompra { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }

        
    }
}
