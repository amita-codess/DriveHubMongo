using DriveHubMongo.DTO;
using DriveHubMongo.Model;
using DriveHubMongo.Repositories;
using DriveHubMongo.Services;
using Microsoft.AspNetCore.Mvc;

namespace DriveHubMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmergencyController : ControllerBase
    {
        private readonly IEmergencyRepository _emergencyRepository;
        private readonly CloudinaryService _cloudinaryService;

        public EmergencyController(
            IEmergencyRepository emergencyRepository,
            CloudinaryService cloudinaryService)
        {
            _emergencyRepository = emergencyRepository;
            _cloudinaryService = cloudinaryService;
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
            string imagePath = "/default-emergency.webp";

            if (dto.Image != null && dto.Image.Length > 0)
            {
                Console.WriteLine($"Image Received: {dto.Image.FileName}");

                var uploadedImage = await _cloudinaryService.UploadImageAsync(dto.Image);

                Console.WriteLine($"Uploaded URL: {uploadedImage}");

                if (!string.IsNullOrWhiteSpace(uploadedImage))
                {
                    imagePath = uploadedImage;
                }
            }
            else
            {
                Console.WriteLine("Image is NULL");
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

            if (dto.Image != null)
            {
                emergency.ImagePath = await _cloudinaryService.UploadImageAsync(dto.Image);
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