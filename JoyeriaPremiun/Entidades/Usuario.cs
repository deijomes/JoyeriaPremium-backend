using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace JoyeriaPremiun.Entidades
{
    public class Usuario
    {
        public int  Id { get; set; } 
        [Required]
        public required string Nombre { get; set; } 
        [EmailAddress]
        public required string Correo { get; set; } 
        public string? Telefono { get; set; } 
        [Required]
        public required string Password { get; set; }
        public bool Estado { get; set; } = true;

        public List<FavoritoProducto> Favoritos { get; set; } = new();
        public List<Venta> productosComprado { get; set; } = new();


    }
}
