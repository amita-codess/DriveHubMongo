using DriveHubMongo.DTO;
using DriveHubMongo.Model;
using DriveHubMongo.Repositories;
using DriveHubMongo.Services;
using Microsoft.AspNetCore.Mvc;

namespace DriveHubMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripRepository _tripRepository;
        private readonly CloudinaryService _cloudinaryService;

        public TripsController(
            ITripRepository tripRepository,
            CloudinaryService cloudinaryService)
        {
            _tripRepository = tripRepository;
            _cloudinaryService = cloudinaryService;
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
            string imagePath = "/default-trip.webp";

            if (dto.Image != null)
            {
                var uploadedImage = await _cloudinaryService.UploadImageAsync(dto.Image);

                if (!string.IsNullOrWhiteSpace(uploadedImage))
                {
                    imagePath = uploadedImage;
                }
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
            trip.UserId = dto.UserId;
            trip.PaymentMethod = dto.PaymentMethod;
            trip.PaymentStatus = dto.PaymentStatus;
            trip.TransactionId = dto.TransactionId;

            if (dto.Image != null && dto.Image.Length > 0)
            {
                trip.ImagePath = await _cloudinaryService.UploadImageAsync(dto.Image);
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

        // GET: api/Trips/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTripsByUserId(string userId)
        {
            var trips = await _tripRepository.GetTripsByUserId(userId);

            return Ok(trips);
        }
    }
}