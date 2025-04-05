namespace NeighborAssessment.Models
{
    public class SearchResult
    {
        public string LocationId { get; set; }
        public List<string> ListingIds { get; set; }
        public int TotalPriceInCents { get; set; }
    }
}
