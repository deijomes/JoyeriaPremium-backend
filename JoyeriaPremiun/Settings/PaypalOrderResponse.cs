using System.Text.Json.Serialization;

namespace JoyeriaPremiun.Settings
{
    public class PaypalOrderResponse

    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("links")]
        public List<Link>? Links { get; set; }
    }

    public class Link
    {
        [JsonPropertyName("href")]
        public string? Href { get; set; }

        [JsonPropertyName("rel")]
        public string? Rel { get; set; }

        [JsonPropertyName("method")]
        public string? Method { get; set; }
    }
}

