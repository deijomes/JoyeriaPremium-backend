using JoyeriaPremiun.Entidades;

namespace JoyeriaPremiun.DTOS
{
    public class productoDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Codigo { get; set; }
        public string? Categoria { get; set; }
        public string? Descripcion { get; set; }
        public decimal PrecioDeVenta  { get; set; }

        public int Stock { get; set; }

        public List<imagenProductoDTO>? imagenProductos { get; set; } = new List<imagenProductoDTO>();
        
    }
}
