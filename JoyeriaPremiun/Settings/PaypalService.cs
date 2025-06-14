﻿
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Microsoft.EntityFrameworkCore;
using JoyeriaPremiun.Datos;
using JoyeriaPremiun.Migrations;
using JoyeriaPremiun.Entidades;

namespace JoyeriaPremiun.Settings
{
    public class PaypalService : IPayPalService
    {
        private readonly HttpClient _httpClient;
        private readonly PayPalSettings _settings;
        private readonly ApplicationDbContext context;

        public PaypalService(IHttpClientFactory factory, IOptions<PayPalSettings> settings, ApplicationDbContext context)
        {
            _httpClient = factory.CreateClient("PaypalClient");         
            _settings = settings.Value;
            this.context = context;
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

        public async Task<string> CaptureOrder(CapturaRequest request)
        {
            
            var venta = await context.ventas
                .Include(v => v.Usuario)
                .ThenInclude(u => u.direcciones)
                .FirstOrDefaultAsync(v => v.PaypalOrderId == request.OrderId);

            if (venta is null)
                throw new Exception("No se encontró la venta asociada a la orden de PayPal.");

            if (venta.Usuario == null)
                throw new Exception("La venta no tiene un usuario asociado.");

            if (venta.Usuario.direcciones == null || !venta.Usuario.direcciones.Any())
                throw new Exception("El usuario no tiene direcciones registradas.");

            var direccion = venta.Usuario.direcciones.First();

            
            var token = await GetAccessTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

           
            var postData = new StringContent("{}", Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(
                $"{_settings.BaseUrl}/v2/checkout/orders/{request.OrderId}/capture",
                postData
            );
            var jsonContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al capturar la orden: {response.StatusCode} - {jsonContent}");
            }

            var captureResponse = JsonSerializer.Deserialize<PaypalOrderResponse>(jsonContent);

           
            venta.Estado = "Pagado";
            venta.FechaDeCompra = DateTime.Now;
            await context.SaveChangesAsync();

            
            var nuevoPedido = new Pedido
            {
                VentaId = venta.Id,
                DireccionId = direccion.Id
            };

            context.Pedidos.Add(nuevoPedido);
            await context.SaveChangesAsync();

            return jsonContent;
        }




        public async Task<string> CreateOrder(CreateOrderRequest request)
        {
            
            var venta = await context.ventas.FindAsync(request.VentaId);

            if (venta is null)
            {
                throw new Exception("La venta no existe.");
            }

            if (venta.Estado == "Pagado")
            {
                throw new Exception("La venta ya fue pagada. No se puede generar una nueva orden.");
            }

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
                    value = request.Amount.ToString("F2", System.Globalization.CultureInfo.InvariantCulture)
                }
            }
        },
                application_context = new
                {
                    brand_name = "JoyeriaPremium",
                    landing_page = "LOGIN",
                    user_action = "PAY_NOW",
                    return_url = request.ReturnUrl,
                    cancel_url = request.CancelUrl
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_settings.BaseUrl}/v2/checkout/orders", content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var paypalResponse = JsonSerializer.Deserialize<PaypalOrderResponse>(result);

            
            venta.PaypalOrderId = paypalResponse.Id;
            await context.SaveChangesAsync();

            return result;
        }

    }
}
