using DriveHubMongo.DTO;
using DriveHubMongo.Model;
using DriveHubMongo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DriveHubMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConstructionController : ControllerBase
    {
        private readonly IConstructionRepository _constructionRepository;
        private readonly IWebHostEnvironment _environment;

        public ConstructionController(
            IConstructionRepository constructionRepository,
            IWebHostEnvironment environment)
        {
            _constructionRepository = constructionRepository;
            _environment = environment;
        }

        // GET: api/Construction
        [HttpGet]
        public async Task<IActionResult> GetAllConstruction()
        {
            var construction = await _constructionRepository.GetAllConstructionAsync();
            return Ok(construction);
        }

        // GET: api/Construction/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConstructionById(string id)
        {
            var construction = await _constructionRepository.GetConstructionByIdAsync(id);

            if (construction == null)
                return NotFound("Construction vehicle not found.");

            return Ok(construction);
        }

        // POST: api/Construction
        [HttpPost]
        public async Task<IActionResult> AddConstruction([FromForm] AddConstructionDto dto)
        {
            string imagePath = "/uploads/default-construction.webp";

            if (dto.Image != null && dto.Image.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");

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

            var construction = new Construction
            {
                VehicleName = dto.VehicleName,
                VehicleNumber = dto.VehicleNumber,
                WorkType = dto.WorkType,
                UserName = dto.UserName,
                ContactNumber = dto.ContactNumber,
                Location = dto.Location,
                Description = dto.Description,
                Status = dto.Status,
                UserId = dto.UserId,
                ImagePath = imagePath,
                PaymentMethod = dto.PaymentMethod,
                PaymentStatus = dto.PaymentStatus,
                TransactionId = dto.TransactionId,
            };

            await _constructionRepository.AddConstructionAsync(construction);

            return Ok(new
            {
                message = "Construction Vehicle Added Successfully",
                construction
            });
        }

        // PUT: api/Construction/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConstruction(string id, [FromForm] AddConstructionDto dto)
        {
            var construction = await _constructionRepository.GetConstructionByIdAsync(id);

            if (construction == null)
                return NotFound("Construction vehicle not found.");

            construction.VehicleName = dto.VehicleName;
            construction.VehicleNumber = dto.VehicleNumber;
            construction.WorkType = dto.WorkType;
            construction.UserName = dto.UserName;
            construction.ContactNumber = dto.ContactNumber;
            construction.Location = dto.Location;
            construction.Description = dto.Description;
            construction.Status = dto.Status;

            if (dto.Image != null && dto.Image.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid() + Path.GetExtension(dto.Image.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }

                construction.ImagePath = "/uploads/" + fileName;
            }

            await _constructionRepository.UpdateConstructionAsync(id, construction);

            return Ok(new
            {
                message = "Construction Vehicle Updated Successfully",
                construction
            });
        }

        // DELETE: api/Construction/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConstruction(string id)
        {
            var construction = await _constructionRepository.GetConstructionByIdAsync(id);

            if (construction == null)
                return NotFound("Construction vehicle not found.");

            await _constructionRepository.DeleteConstructionAsync(id);

            return Ok("Construction Vehicle Deleted Successfully");
        }

        // DELETE: api/Construction/DeleteAll
        [HttpDelete("DeleteAll")]
        public async Task<IActionResult> DeleteAll()
        {
            await _constructionRepository.DeleteAllConstructionAsync();
            return Ok("All construction records deleted.");
        }

        // GET: api/Construction/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetConstructionByUserId(string userId)
        {
            var constructionVehicles = await _constructionRepository.GetConstructionByUserId(userId);

            return Ok(constructionVehicles);
        }
    }
}