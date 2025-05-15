namespace JoyeriaPremiun.Settings
{
    public interface IPayPalService
    {
        Task<string> CreateOrder(decimal amount);
        Task<string> CaptureOrder(string orderId);
    }
}
