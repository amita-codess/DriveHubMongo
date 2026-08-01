using DriveHubMongo.DTO;
using DriveHubMongo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DriveHubMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepository _chatRepository;

        public ChatController(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(ChatSearchRequestDto request)
        {
            var result = await _chatRepository.SearchVehiclesAsync(
                request.VehicleName,
                request.Location);

            return Ok(result);
        }
    }
}
