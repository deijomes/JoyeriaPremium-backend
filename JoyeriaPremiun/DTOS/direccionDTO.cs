using JoyeriaPremiun.Entidades;
using System.ComponentModel.DataAnnotations;

namespace JoyeriaPremiun.DTOS
{
    public class direccionDTO
    {
        public int Id { get; set; }

        public string? Ciudad { get; set; }
        public string? Calle { get; set; }
        public string? Carrera { get; set; }
        public string? NumeroTelefono { get; set; }

        public usuarioDTO? Usuario { get; set; }
    }
}
