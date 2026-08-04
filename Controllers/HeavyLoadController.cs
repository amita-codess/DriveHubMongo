using DriveHubMongo.DTO;
using DriveHubMongo.Model;
using DriveHubMongo.Repositories;
using DriveHubMongo.Services;
using Microsoft.AspNetCore.Mvc;

namespace DriveHubMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeavyLoadController : ControllerBase
    {
        private readonly IHeavyLoadRepository _repository;
        private readonly CloudinaryService _cloudinaryService;

        public HeavyLoadController(
            IHeavyLoadRepository repository,
            CloudinaryService cloudinaryService)
        {
            _repository = repository;
            _cloudinaryService = cloudinaryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var vehicle = await _repository.GetByIdAsync(id);

            if (vehicle == null)
                return NotFound();

            return Ok(vehicle);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(string userId)
        {
            return Ok(await _repository.GetByUserIdAsync(userId));
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] AddHeavyLoadDto dto,
            IFormFile? image)
        {
            try
            {
                string imagePath = "/default-heavyload.webp";

                if (image != null)
                {
                    var uploadedImage = await _cloudinaryService.UploadImageAsync(image);

                    if (!string.IsNullOrWhiteSpace(uploadedImage))
                    {
                        imagePath = uploadedImage;
                    }
                }

                var heavyLoad = new HeavyLoad
                {
                    UserId = dto.UserId,
                    VehicleName = dto.VehicleName,
                    VehicleNumber = dto.VehicleNumber,
                    Category = "HeavyLoad",
                    Location = dto.Location,
                    OwnerName = dto.OwnerName,
                    OwnerContact = dto.OwnerContact,
                    LoadCapacity = dto.LoadCapacity,
                    Description = dto.Description,
                    PaymentMethod = dto.PaymentMethod,
                    PaymentStatus = dto.PaymentStatus,
                    TransactionId = dto.TransactionId,
                    ImagePath = imagePath
                };

                await _repository.CreateAsync(heavyLoad);

                return Ok(new
                {
                    message = "Heavy Load Vehicle Added Successfully",
                    data = heavyLoad
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
            [FromForm] AddHeavyLoadDto dto,
            IFormFile? image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existing = await _repository.GetByIdAsync(id);

            if (existing == null)
                return NotFound();

            string imagePath = existing.ImagePath;

            if (image != null)
            {
                var uploadedImage = await _cloudinaryService.UploadImageAsync(image);

                if (!string.IsNullOrWhiteSpace(uploadedImage))
                {
                    imagePath = uploadedImage;
                }
            }

            var heavyLoad = new HeavyLoad
            {
                Id = id,
                UserId = existing.UserId,
                VehicleName = dto.VehicleName,
                VehicleNumber = dto.VehicleNumber,
                Category = "HeavyLoad",
                Location = dto.Location,
                OwnerName = dto.OwnerName,
                OwnerContact = dto.OwnerContact,
                LoadCapacity = dto.LoadCapacity,
                Description = dto.Description,
                PaymentMethod = existing.PaymentMethod,
                PaymentStatus = existing.PaymentStatus,
                TransactionId = existing.TransactionId,
                ImagePath = imagePath
            };

            await _repository.UpdateAsync(id, heavyLoad);

            return Ok(new
            {
                message = "Heavy Load Updated Successfully"
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _repository.GetByIdAsync(id);

            if (existing == null)
                return NotFound();

            await _repository.DeleteAsync(id);

            return Ok(new
            {
                message = "Heavy Load Deleted Successfully"
            });
        }
    }
}