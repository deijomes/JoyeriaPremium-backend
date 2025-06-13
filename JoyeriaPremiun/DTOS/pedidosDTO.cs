using JoyeriaPremiun.Entidades;

namespace JoyeriaPremiun.DTOS
{
    public class pedidosDTO
    {
        public int Id { get; set; }
        public DateTime FechaPedido { get; set; }
        public string Estado { get; set; }

        public ventaDTO? Venta { get; set; } // usa DTO, no entidad
        public direccionPedidoDTO Direccion { get; set; }// crea si no existe
        public usuarioDTO? Usuario { get; set; }
    }
}
