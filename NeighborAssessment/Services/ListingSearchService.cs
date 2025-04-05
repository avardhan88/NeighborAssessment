using NeighborAssessment.Models;
using System.Text.Json;

namespace NeighborAssessment.Services
{
    public class ListingSearchService
    {
        private readonly List<Listing> _listings;

        public ListingSearchService()
        {
            var json = File.ReadAllText("listings.json");
            //var json = File.ReadAllText("test_listings.json");
            _listings = JsonSerializer.Deserialize<List<Listing>>(json) ?? new();           
        }

        public List<SearchResult> Search(List<VehicleRequest> requests)
        {
            var groupedByLocation = _listings.GroupBy(l => l.LocationId);
            var results = new List<SearchResult>();

            foreach (var locationGroup in groupedByLocation)
            {
                var locationListings = locationGroup.ToList();
                var combinations = GetCheapestCombination(locationListings, requests);
                if (combinations != null)
                    results.Add(combinations);
            }

            return results.OrderBy(r => r.TotalPriceInCents).ToList();
        }

        private SearchResult? GetCheapestCombination(List<Listing> listings, List<VehicleRequest> requests)
        {
            var allCombos = new List<List<Listing>>();
            GetCombinationsRecursive(listings, requests, new List<Listing>(), allCombos);

            var best = allCombos
                .OrderBy(c => c.Sum(l => l.PriceInCents))
                .FirstOrDefault();

            if (best == null) return null;

            return new SearchResult
            {
                LocationId = best.First().LocationId,
                ListingIds = best.Select(l => l.Id).ToList(),
                TotalPriceInCents = best.Sum(l => l.PriceInCents)
            };
        }

        private void GetCombinationsRecursive(List<Listing> available, List<VehicleRequest> remaining, List<Listing> current, List<List<Listing>> result)
        {
            if (!remaining.Any())
            {
                result.Add(new List<Listing>(current));
                return;
            }

            var req = remaining.First();
            var matches = available
                .Where(l => l.Length >= req.Length && l.Width >= 10)
                .ToList();

            var combos = GetKCombinations(matches, req.Quantity);
            foreach (var combo in combos)
            {
                var nextAvailable = available.Except(combo).ToList();
                var nextRemaining = remaining.Skip(1).ToList();
                var nextCurrent = new List<Listing>(current);
                nextCurrent.AddRange(combo);
                GetCombinationsRecursive(nextAvailable, nextRemaining, nextCurrent, result);
            }
        }

        private IEnumerable<List<Listing>> GetKCombinations(List<Listing> listings, int k)
        {
            if (k == 0) yield return new List<Listing>();
            else
            {
                for (int i = 0; i < listings.Count; i++)
                {
                    foreach (var tail in GetKCombinations(listings.Skip(i + 1).ToList(), k - 1))
                    {
                        var combo = new List<Listing> { listings[i] };
                        combo.AddRange(tail);
                        yield return combo;
                    }
                }
            }
        }
    }
}
