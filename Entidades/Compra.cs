using System.ComponentModel.DataAnnotations;

namespace JoyeriaPremiun.Entidades
{
    public class Compra
    {
        public int Id { get; set; }
        [Required]
        public required string? proveedor { get; set; }
        public DateTime FechaDeCompra { get; set; } = DateTime.Now;


        public List<CompraProductoS>? ProductosComprados { get; set; } = new List<CompraProductoS>();
    } 
}
