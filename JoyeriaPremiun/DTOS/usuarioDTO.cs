using System.ComponentModel.DataAnnotations;

namespace JoyeriaPremiun.DTOS
{
    public class usuarioDTO
    {
        public string Id { get; set; } 
        [Required]
        public required string Nombre { get; set; }
        [EmailAddress]
        public required string Correo { get; set; }
        public string? Telefono { get; set; }
        
    }
}
