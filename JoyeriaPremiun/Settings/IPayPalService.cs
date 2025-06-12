namespace JoyeriaPremiun.Settings
{
    public interface IPayPalService
    {
        Task<string> CreateOrder(CreateOrderRequest request);
        Task<string> CaptureOrder(CapturaRequest request);
    }
}
