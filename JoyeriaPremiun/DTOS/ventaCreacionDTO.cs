using JoyeriaPremiun.Entidades;
using System.ComponentModel.DataAnnotations;

namespace JoyeriaPremiun.DTOS
{
    public class ventaCreacionDTO
    {

        [Required]
        public required int usuarioId { get; set; }
        public List<ventaProductoDTO>? productos { get; set; }


    }
}
