using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeighborAssessment.Models;
using NeighborAssessment.Services;

namespace NeighborAssessment.Controllers
{
    [ApiController]
    [Route("/")]
    public class ListingsController : ControllerBase
    {
        private readonly ListingSearchService _service;

        public ListingsController(ListingSearchService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Post([FromBody] List<VehicleRequest> requests)
        {
            var result = _service.Search(requests);
            return Ok(result);
        }
    }
}
