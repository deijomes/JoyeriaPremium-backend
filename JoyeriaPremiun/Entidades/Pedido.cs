namespace JoyeriaPremiun.Entidades
{
    public class Pedido
    {
        public int Id { get; set; }

        public int VentaId { get; set; }
        public Venta? Venta { get; set; }

        public int DireccionId { get; set; } 
        public Direccion? Direccion { get; set; }

        public PedidoEstado Estado { get; set; } = PedidoEstado.Pendiente;

        
    }

    public enum PedidoEstado
    {
        Pendiente,
        Confirmado,
        Enviado,
        Entregado,
        Cancelado
    }
}
