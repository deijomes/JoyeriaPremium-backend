using System.ComponentModel.DataAnnotations;

namespace JoyeriaPremiun.DTOS
{
    public class compraCreacionDTO
    {
        [Required]
        public required string? proveedor { get; set; }
        public List<compraDTO>? productos { get; set; }
    }
}
