namespace JoyeriaPremiun.DTOS
{
    public class favoritosDTO
    {
        
        public int UsuarioId { get; set; }

        public List<productoDTO>? favoritos { get; set; }
    }
}
