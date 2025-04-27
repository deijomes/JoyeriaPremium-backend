using JoyeriaPremiun.Entidades;
using System.ComponentModel.DataAnnotations;

namespace JoyeriaPremiun.DTOS
{
    public class compraDTO
    {
        [Required]
        public required string NombreProducto { get; set; }
        public string? CodigoProducto { get; set; }
        public decimal PrecioDeCompra { get; set; }
        public int Cantidad { get; set; }
        public decimal Total => PrecioDeCompra * Cantidad;
    }
}
