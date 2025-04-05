namespace NeighborAssessment.Models
{
    public class Listing
    {
        public string Id { get; set; }
        public string LocationId { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int PriceInCents { get; set; }
    }
}
