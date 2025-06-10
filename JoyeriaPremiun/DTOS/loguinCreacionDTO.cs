using System.ComponentModel.DataAnnotations;

namespace JoyeriaPremiun.DTOS
{
    public class loguinCreacionDTO
    {
        [EmailAddress]
        public required string Correo { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
