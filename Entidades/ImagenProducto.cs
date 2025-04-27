using System.ComponentModel.DataAnnotations;

namespace JoyeriaPremiun.Entidades
{
    public class ImagenProducto
    {
        public int id { get; set; }
        public int ProductoId { get; set; }
        public string? foto { get; set; }
        
        public Producto? producto { get; set;  }
    }
}
