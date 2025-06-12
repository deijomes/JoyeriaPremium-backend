using JoyeriaPremiun.Entidades;
using System.ComponentModel.DataAnnotations;

namespace JoyeriaPremiun.DTOS
{
    public class direccionCreacionDTO
    {

        public string? UsuarioId { get; set; }
        public string? Ciudad { get; set; }
        public string? Calle { get; set; }
        public string? Carrera { get; set; }
      

    }
}
