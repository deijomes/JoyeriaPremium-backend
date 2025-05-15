
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace JoyeriaPremiun.Settings
{
    public class PaypalService : IPayPalService
    {
        private readonly HttpClient _httpClient;
        private readonly PayPalSettings _settings;

        public PaypalService(IHttpClientFactory factory, IOptions<PayPalSettings> settings)
        {
            _httpClient = factory.CreateClient("PaypalClient");         
            _settings = settings.Value;

        }


        private async Task<string> GetAccessTokenAsync()
        {
            var authBytes = Encoding.UTF8.GetBytes($"{_settings.ClientId}:{_settings.Secret}");
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authBytes));

            var content = new FormUrlEncodedContent(new[]
            {
              new KeyValuePair<string, string>("grant_type", "client_credentials")
    });

            var response = await _httpClient.PostAsync("/v1/oauth2/token", content);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            using var json = JsonDocument.Parse(result);
            return json.RootElement.GetProperty("access_token").GetString()!;
        }

        public async Task<string> CaptureOrder(string orderId)
        {
            var token = await GetAccessTokenAsync();

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            // PayPal acepta un body vacío como {}
            var postData = new StringContent("{}", Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_settings.BaseUrl}/v2/checkout/orders/{orderId}/capture", postData);
            var jsonContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al capturar la orden: {response.StatusCode} - {jsonContent}");
            }

            return jsonContent;
        }


        public async Task<string> CreateOrder(decimal amount)
        {

            var accessToken = await GetAccessTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            var body = new
            {
                intent = "CAPTURE",
                purchase_units = new[]
                {
            new
            {
                amount = new
                {
                    currency_code = "USD",
                    value = amount.ToString("F2", System.Globalization.CultureInfo.InvariantCulture)
                }
            }
        },
                application_context = new
                {
                    return_url = "https://localhost:4200/pago-exitoso",   // Cambia por tu URL real
                    cancel_url = "https://localhost:4200/pago-cancelado"
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_settings.BaseUrl}/v2/checkout/orders", content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
