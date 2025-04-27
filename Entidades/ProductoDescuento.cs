namespace JoyeriaPremiun.Entidades
{
    public class ProductoDescuento
    {
        public int id { get; set; }
        public int ProductoId { get; set; }
        public decimal? Descuento { get; set; }

        public Producto? producto { get; set; }
    }
}
