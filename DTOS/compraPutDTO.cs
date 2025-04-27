using System.ComponentModel.DataAnnotations;

namespace JoyeriaPremiun.DTOS
{
    public class compraPutDTO
    {
        public int Id { get; set; }
        [Required]
        public required string? proveedor { get; set; }
        public DateTime FechaDeCompra { get; set; }

        public List<compraProductoPutDTO>? ProductosComprados { get; set; } = new List<compraProductoPutDTO>();
    }
}
