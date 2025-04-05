using System.Text.Json.Serialization;

namespace NeighborAssessment.Models
{
    public class Listing
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("location_id")]
        public string LocationId { get; set; }

        [JsonPropertyName("length")]
        public int Length { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("price_in_cents")]
        public int PriceInCents { get; set; }
    }
}
