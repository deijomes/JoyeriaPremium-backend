using System.ComponentModel.DataAnnotations;

namespace JoyeriaPremiun.Entidades
{
    public class Pedido
    {
        public int Id { get; set; }

        
        public int? VentaId { get; set; }

       
        public int? DireccionId { get; set; }

        public DateTime FechaPedido { get; set; } = DateTime.Now;

        public string Estado { get; set; } = "Por despachar";

        
        public Venta? Venta { get; set; }
        public Direccion? Direccion { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
