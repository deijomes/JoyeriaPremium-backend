using JoyeriaPremiun.Entidades;
using System.ComponentModel.DataAnnotations;

namespace JoyeriaPremiun.DTOS
{
    public class ventaDTO
    {

        public int Id { get; set; }
        [Required]
        public DateTime FechaDeCompra { get; set; } = DateTime.Now;

        public List<ventaProductDTO>? VentaProductos { get; set; } = new List<ventaProductDTO>();
        public usuarioDTO? Usuario { get; set; }

        public decimal total { get; set; }
    }
}
