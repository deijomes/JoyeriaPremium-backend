using JoyeriaPremiun.Entidades;
using System.ComponentModel.DataAnnotations;

namespace JoyeriaPremiun.DTOS
{
    public class compraGetDTO
    {
        public int Id { get; set; }
        [Required]
        public required string? proveedor { get; set; }
        public DateTime FechaDeCompra { get; set; }

        public List<compraProductoDTO>? ProductosComprados { get; set; } = new List<compraProductoDTO>();
    }
}
