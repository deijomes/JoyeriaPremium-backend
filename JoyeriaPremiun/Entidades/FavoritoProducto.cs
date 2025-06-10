using JoyeriaPremiun.DTOS;

namespace JoyeriaPremiun.Entidades
{
    public class FavoritoProducto
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public Producto? Productos { get; set; }
    }
}
