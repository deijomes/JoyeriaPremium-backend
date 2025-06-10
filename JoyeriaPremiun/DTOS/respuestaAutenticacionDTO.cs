using JoyeriaPremiun.Entidades;

namespace JoyeriaPremiun.DTOS
{
    public class respuestaAutenticacionDTO
    {
        public required string token { get; set; }
        public DateTime expiracion { get; set; }
        public  required string userID { get; set; }
        public  required string usuario { get; set; }
        public required string role { get; set; } 
    }
}
