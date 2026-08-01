using DriveHubMongo.DTO;
using DriveHubMongo.Model;
using DriveHubMongo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DriveHubMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmergencyController : ControllerBase
    {
        private readonly IEmergencyRepository _emergencyRepository;
        private readonly IWebHostEnvironment _environment;

        public EmergencyController(
            IEmergencyRepository emergencyRepository,
            IWebHostEnvironment environment)
        {
            _emergencyRepository = emergencyRepository;
            _environment = environment;
        }

        // GET: api/Emergency
        [HttpGet]
        public async Task<IActionResult> GetAllEmergency()
        {
            var emergency = await _emergencyRepository.GetAllEmergencyAsync();
            return Ok(emergency);
        }

        // GET: api/Emergency/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmergencyById(string id)
        {
            var emergency = await _emergencyRepository.GetEmergencyByIdAsync(id);

            if (emergency == null)
                return NotFound("Emergency vehicle not found.");

            return Ok(emergency);
        }

        // POST: api/Emergency
        [HttpPost]
        public async Task<IActionResult> AddEmergency([FromForm] AddEmergencyDto dto)
        {
            string imagePath = "/uploads/default-emergency.webp";

            if (dto.Image != null && dto.Image.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Guid.NewGuid() + Path.GetExtension(dto.Image.FileName);

                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }

                imagePath = "/uploads/" + fileName;
            }

            var emergency = new Emergency
            {
                VehicleName = dto.VehicleName,
                VehicleNumber = dto.VehicleNumber,
                VehicleType = dto.VehicleType,
                DriverName = dto.DriverName,
                DriverContact = dto.DriverContact,
                Location = dto.Location,
                Availability = dto.Availability,
                UserId = dto.UserId,
                ImagePath = imagePath,
                PaymentMethod = dto.PaymentMethod,
                PaymentStatus = dto.PaymentStatus,
                TransactionId = dto.TransactionId,
            };

            await _emergencyRepository.AddEmergencyAsync(emergency);

            return Ok(new
            {
                message = "Emergency Vehicle Added Successfully",
                emergency
            });
        }

        // PUT: api/Emergency/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmergency(string id, [FromForm] AddEmergencyDto dto)
        {
            var emergency = await _emergencyRepository.GetEmergencyByIdAsync(id);

            if (emergency == null)
                return NotFound("Emergency vehicle not found.");

            emergency.VehicleName = dto.VehicleName;
            emergency.VehicleNumber = dto.VehicleNumber;
            emergency.VehicleType = dto.VehicleType;
            emergency.DriverName = dto.DriverName;
            emergency.DriverContact = dto.DriverContact;
            emergency.Location = dto.Location;
            emergency.Availability = dto.Availability;

            if (dto.Image != null && dto.Image.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Guid.NewGuid() + Path.GetExtension(dto.Image.FileName);

                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }

                emergency.ImagePath = "/uploads/" + fileName;
            }

            await _emergencyRepository.UpdateEmergencyAsync(id, emergency);

            return Ok(new
            {
                message = "Emergency Vehicle Updated Successfully",
                emergency
            });
        }

        // DELETE: api/Emergency/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmergency(string id)
        {
            var emergency = await _emergencyRepository.GetEmergencyByIdAsync(id);

            if (emergency == null)
                return NotFound("Emergency vehicle not found.");

            await _emergencyRepository.DeleteEmergencyAsync(id);

            return Ok("Emergency Vehicle Deleted Successfully");
        }

        // DELETE: api/Emergency/DeleteAll
        [HttpDelete("DeleteAll")]
        public async Task<IActionResult> DeleteAll()
        {
            await _emergencyRepository.DeleteAllEmergencyAsync();
            return Ok("All emergency records deleted.");
        }

        // GET: api/Emergency/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetEmergencyByUserId(string userId)
        {
            var emergencyVehicles = await _emergencyRepository.GetEmergencyByUserId(userId);

            return Ok(emergencyVehicles);
        }
    }
}