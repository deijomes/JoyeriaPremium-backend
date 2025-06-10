using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace JoyeriaPremiun.Entidades
{
    public class Usuario: IdentityUser
    {
        [Required]
       
        public bool Estado { get; set; } = true;

        public List<FavoritoProducto> Favoritos { get; set; } = new();
        public List<Venta> productosComprado { get; set; } = new();
        public List<Direccion> direcciones { get; set; } = new();


    }
}
