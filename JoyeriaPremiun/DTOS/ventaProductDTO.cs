namespace JoyeriaPremiun.DTOS
{
    public class ventaProductDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Codigo { get; set; }
        public decimal PrecioDeVenta { get; set; }
        public int? Cantidad { get; set; }

        public decimal Total
        {
            get
            {
                return PrecioDeVenta * (Cantidad ?? 0);
            }
        }



    }
}
