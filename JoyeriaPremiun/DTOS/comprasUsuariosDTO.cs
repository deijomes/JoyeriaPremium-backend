namespace JoyeriaPremiun.DTOS
{
    public class comprasUsuariosDTO
    {
        public int UsuarioId { get; set; }

        public List<productoDTO>? productosComprados  { get; set; }
    }
}
