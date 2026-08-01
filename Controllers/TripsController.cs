using DriveHubMongo.DTO;
using DriveHubMongo.Model;
using DriveHubMongo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DriveHubMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripRepository _tripRepository;
        private readonly IWebHostEnvironment _environment;

        public TripsController(
            ITripRepository tripRepository,
            IWebHostEnvironment environment)
        {
            _tripRepository = tripRepository;
            _environment = environment;
        }

        // GET: api/Trips
        [HttpGet]
        public async Task<IActionResult> GetAllTrips()
        {
            var trips = await _tripRepository.GetAllTripsAsync();
            return Ok(trips);
        }

        // GET: api/Trips/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTripById(string id)
        {
            var trip = await _tripRepository.GetTripByIdAsync(id);

            if (trip == null)
                return NotFound("Trip not found.");

            return Ok(trip);
        }

        // POST: api/Trips
        [HttpPost]
        public async Task<IActionResult> AddTrip([FromForm] AddTripDto dto)
        {
            string imagePath = "/uploads/default-trip.webp";

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

            var trip = new Trip
            {
                VehicleName = dto.VehicleName,
                VehicleNumber = dto.VehicleNumber,
                Category = dto.Category,
                Location = dto.Location,
                OwnerName = dto.OwnerName,
                OwnerContact = dto.OwnerContact,
                SeatingCapacity = dto.SeatingCapacity,
                ACAvailable = dto.ACAvailable,
                UserId = dto.UserId,
                ImagePath = imagePath,
                PaymentMethod = dto.PaymentMethod,
                PaymentStatus = dto.PaymentStatus,
                TransactionId = dto.TransactionId,
            };

            await _tripRepository.AddTripAsync(trip);

            return Ok(new
            {
                message = "Trip Added Successfully",
                trip
            });
        }

        // PUT: api/Trips/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrip(string id, [FromForm] AddTripDto dto)
        {
            var trip = await _tripRepository.GetTripByIdAsync(id);

            if (trip == null)
                return NotFound("Trip not found.");

            trip.VehicleName = dto.VehicleName;
            trip.VehicleNumber = dto.VehicleNumber;
            trip.Category = dto.Category;
            trip.Location = dto.Location;
            trip.OwnerName = dto.OwnerName;
            trip.OwnerContact = dto.OwnerContact;
            trip.SeatingCapacity = dto.SeatingCapacity;
            trip.ACAvailable = dto.ACAvailable;


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

                trip.ImagePath = "/uploads/" + fileName;
            }

            await _tripRepository.UpdateTripAsync(id, trip);

            return Ok(new
            {
                message = "Trip Updated Successfully",
                trip
            });
        }

        // DELETE: api/Trips/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip(string id)
        {
            var trip = await _tripRepository.GetTripByIdAsync(id);

            if (trip == null)
                return NotFound("Trip not found.");

            await _tripRepository.DeleteTripAsync(id);

            return Ok("Trip Deleted Successfully");
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTripsByUserId(string userId)
        {
            var trips = await _tripRepository.GetTripsByUserId(userId);

            return Ok(trips);
        }
    }
}