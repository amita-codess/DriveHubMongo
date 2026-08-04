using DriveHubMongo.DTO;
using DriveHubMongo.Model;
using DriveHubMongo.Repositories;
using DriveHubMongo.Services;
using Microsoft.AspNetCore.Mvc;

namespace DriveHubMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgricultureController : ControllerBase
    {
        private readonly IAgricultureRepository _repository;
        private readonly CloudinaryService _cloudinaryService;

        public AgricultureController(
            IAgricultureRepository repository,
            CloudinaryService cloudinaryService)
        {
            _repository = repository;
            _cloudinaryService = cloudinaryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Agriculture>>> GetAll()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Agriculture>> GetById(string id)
        {
            var agriculture = await _repository.GetByIdAsync(id);

            if (agriculture == null)
                return NotFound();

            return Ok(agriculture);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(string userId)
        {
            return Ok(await _repository.GetByUserIdAsync(userId));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AddAgricultureDto dto)
        {
            try
            {
                string imagePath = "/default-agriculture.webp";

                if (dto.Image != null)
                {
                    imagePath = await _cloudinaryService.UploadImageAsync(dto.Image);
                }

                var agriculture = new Agriculture
                {
                    UserId = dto.UserId,
                    VehicleName = dto.VehicleName,
                    VehicleNumber = dto.VehicleNumber,
                    Category = dto.Category,
                    Location = dto.Location,
                    OwnerName = dto.OwnerName,
                    OwnerContact = dto.OwnerContact,
                    Specifications = dto.Specifications,
                    ImagePath = imagePath,
                    PaymentMethod = dto.PaymentMethod,
                    PaymentStatus = dto.PaymentStatus,
                    TransactionId = dto.TransactionId
                };

                await _repository.CreateAsync(agriculture);

                return Ok(new
                {
                    message = "Agriculture Vehicle Added Successfully",
                    data = agriculture
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            string id,
            [FromForm] AddAgricultureDto dto)
        {
            var agriculture = await _repository.GetByIdAsync(id);

            if (agriculture == null)
                return NotFound("Agriculture vehicle not found.");

            agriculture.VehicleName = dto.VehicleName;
            agriculture.VehicleNumber = dto.VehicleNumber;
            agriculture.Category = dto.Category;
            agriculture.Location = dto.Location;
            agriculture.OwnerName = dto.OwnerName;
            agriculture.OwnerContact = dto.OwnerContact;
            agriculture.Specifications = dto.Specifications;

            if (dto.Image != null)
            {
                agriculture.ImagePath =
                    await _cloudinaryService.UploadImageAsync(dto.Image);
            }

            await _repository.UpdateAsync(id, agriculture);

            return Ok(new
            {
                message = "Agriculture Vehicle Updated Successfully",
                data = agriculture
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var agriculture = await _repository.GetByIdAsync(id);

            if (agriculture == null)
                return NotFound();

            await _repository.DeleteAsync(id);

            return Ok(new
            {
                message = "Agriculture Vehicle Deleted Successfully"
            });
        }
    }
}