using DriveHubMongo.DTO;
using DriveHubMongo.Model;
using DriveHubMongo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DriveHubMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LightLoadController : ControllerBase
    {
        private readonly ILightLoadRepository _repository;
        private readonly IWebHostEnvironment _environment;

        public LightLoadController(
            ILightLoadRepository repository,
            IWebHostEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
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
            [FromForm] AddLightLoadDto dto,
            IFormFile? image)
        {
            try
            {
                var lightLoad = new LightLoad
                {
                    UserId = dto.UserId,
                    VehicleName = dto.VehicleName,
                    VehicleNumber = dto.VehicleNumber,
                    Category = "LightLoad",
                    Location = dto.Location,
                    OwnerName = dto.OwnerName,
                    OwnerContact = dto.OwnerContact,
                    LoadCapacity = dto.LoadCapacity,
                    Description = dto.Description,
                    PaymentMethod = dto.PaymentMethod,
                    PaymentStatus = dto.PaymentStatus,
                    TransactionId = dto.TransactionId,
                    ImagePath = "/uploads/default-lightload.webp"
                };

                if (image != null)
                {
                    var uploadPath = Path.Combine(
                        _environment.WebRootPath,
                        "uploads"
                    );

                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);

                    var fileName = Guid.NewGuid() +
                                   Path.GetExtension(image.FileName);

                    var filePath = Path.Combine(
                        uploadPath,
                        fileName
                    );

                    using var stream = new FileStream(
                        filePath,
                        FileMode.Create
                    );

                    await image.CopyToAsync(stream);

                    lightLoad.ImagePath = "/uploads/" + fileName;
                }

                await _repository.CreateAsync(lightLoad);

                return Ok(new
                {
                    message = "Light Load Vehicle Added Successfully",
                    data = lightLoad
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
            [FromForm] AddLightLoadDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);

            if (existing == null)
                return NotFound();

            var lightLoad = new LightLoad
            {
                Id = id,
                UserId = existing.UserId,
                VehicleName = dto.VehicleName,
                VehicleNumber = dto.VehicleNumber,
                Category = "LightLoad",
                Location = dto.Location,
                OwnerName = dto.OwnerName,
                OwnerContact = dto.OwnerContact,
                LoadCapacity = dto.LoadCapacity,
                Description = dto.Description,
                PaymentMethod = existing.PaymentMethod,
                PaymentStatus = existing.PaymentStatus,
                TransactionId = existing.TransactionId,
                ImagePath = existing.ImagePath
            };

            await _repository.UpdateAsync(id, lightLoad);

            return Ok(new
            {
                message = "Light Load Updated Successfully"
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
                message = "Light Load Deleted Successfully"
            });
        }
    }
}
