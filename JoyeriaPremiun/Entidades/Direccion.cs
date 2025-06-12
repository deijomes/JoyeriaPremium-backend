using System.ComponentModel.DataAnnotations;

namespace JoyeriaPremiun.Entidades
{
    public class Direccion
    {
        public int Id { get; set; }

        [Required]
        public string? UsuarioId { get; set; }
        public string? Ciudad { get; set; }
        public string? Calle { get; set; }
        public string? Carrera { get; set; }  
      
        public Usuario? Usuario { get; set; }
        
       
    }
}
