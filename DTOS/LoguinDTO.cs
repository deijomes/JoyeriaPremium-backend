using System.ComponentModel.DataAnnotations;

namespace JoyeriaPremiun.DTOS
{
    public class LoguinDTO
    {
        [Required]
        public required string Correo { get; set; }
        
        [Required]
        public required string Password { get; set; }
    }
}
