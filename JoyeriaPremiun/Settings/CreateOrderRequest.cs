namespace JoyeriaPremiun.Settings
{
    public class CreateOrderRequest
    {
        public int? VentaId { get; set; }
        public decimal Amount { get; set; }
        public string? ReturnUrl { get; set; }
        public string? CancelUrl { get; set; }
        
    }
}
