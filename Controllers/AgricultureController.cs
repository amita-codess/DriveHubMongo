using DriveHubMongo.DTO;
using DriveHubMongo.Model;
using DriveHubMongo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DriveHubMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgricultureController : ControllerBase
    {
        private readonly IAgricultureRepository _repository;

        public AgricultureController(IAgricultureRepository repository)
        {
            _repository = repository;
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

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] AddAgricultureDto dto)
        {
            string imagePath = "/uploads/default-agriculture.webp";

            if (dto.Image != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid() + Path.GetExtension(dto.Image.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }

                imagePath = "/uploads/" + fileName;
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
                message = "Agriculture vehicle added successfully."
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromForm] AddAgricultureDto dto)
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

            if (dto.Image != null && dto.Image.Length > 0)
            {
                var uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "uploads");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid() +
                               Path.GetExtension(dto.Image.FileName);

                var filePath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await dto.Image.CopyToAsync(stream);

                agriculture.ImagePath = "/uploads/" + fileName;
            }

            await _repository.UpdateAsync(id, agriculture);

            return Ok(new
            {
                message = "Agriculture vehicle updated successfully.",
                agriculture
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _repository.DeleteAsync(id);
            return Ok(new
            {
                message = "Agriculture vehicle deleted successfully."
            });
        }
    }
}
