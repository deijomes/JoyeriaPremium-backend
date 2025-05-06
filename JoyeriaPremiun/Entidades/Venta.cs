using System.ComponentModel.DataAnnotations;

namespace JoyeriaPremiun.Entidades
{
    public class Venta
    {
      
            public int Id { get; set; }
            [Required]
            public required int usuarioId { get; set; }
            public DateTime FechaDeCompra { get; set; } = DateTime.Now;

            public List<VentaProducto>? VentaProductos { get; set; } = new List<VentaProducto>();
            public Usuario? usuario { get; set; }

            public decimal total { get; set; }

    }
}

