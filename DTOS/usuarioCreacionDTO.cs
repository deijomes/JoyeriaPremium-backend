using System.ComponentModel.DataAnnotations;

namespace JoyeriaPremiun.DTOS
{
    public class usuarioCreacionDTO
    {
        [Required]
        public required string Nombre { get; set; }
        [EmailAddress]
        public required string Correo { get; set; }
        public string? Telefono { get; set; }
        [Required]
        public required string Password { get; set; }
       
    }
}
