using DriveHubMongo.DTO;
using DriveHubMongo.Model;
using DriveHubMongo.Repositories;
using DriveHubMongo.Services;
using Microsoft.AspNetCore.Mvc;

namespace DriveHubMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalCarController : ControllerBase
    {
        private readonly IRentalCarRepository _repository;
        private readonly CloudinaryService _cloudinaryService;

        public RentalCarController(
            IRentalCarRepository repository,
            CloudinaryService cloudinaryService)
        {
            _repository = repository;
            _cloudinaryService = cloudinaryService;
        }

        // GET: api/RentalCar
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cars = await _repository.GetAllAsync();
            return Ok(cars);
        }

        // GET: api/RentalCar/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var car = await _repository.GetByIdAsync(id);

            if (car == null)
                return NotFound();

            return Ok(car);
        }

        // GET: api/RentalCar/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var cars = await _repository.GetByUserIdAsync(userId);
            return Ok(cars);
        }

        // POST: api/RentalCar
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AddRentalCarDto dto)
        {
            try
            {
                string imagePath = "/default-rentalcar.webp";

                if (dto.Image != null)
                {
                    imagePath = await _cloudinaryService.UploadImageAsync(dto.Image);
                }

                var rentalCar = new RentalCar
                {
                    UserId = dto.UserId,
                    VehicleName = dto.VehicleName,
                    VehicleNumber = dto.VehicleNumber,
                    Category = dto.Category,
                    Location = dto.Location,
                    OwnerName = dto.OwnerName,
                    OwnerContact = dto.OwnerContact,
                    SeatingCapacity = dto.SeatingCapacity,
                    ACAvailable = dto.ACAvailable,
                    ImagePath = imagePath,
                    PaymentMethod = dto.PaymentMethod,
                    PaymentStatus = dto.PaymentStatus,
                    TransactionId = dto.TransactionId
                };

                await _repository.CreateAsync(rentalCar);

                return Ok(new
                {
                    message = "Rental Car Added Successfully",
                    data = rentalCar
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/RentalCar/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            string id,
            [FromForm] AddRentalCarDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);

            if (existing == null)
                return NotFound();

            existing.VehicleName = dto.VehicleName;
            existing.VehicleNumber = dto.VehicleNumber;
            existing.Category = dto.Category;
            existing.Location = dto.Location;
            existing.OwnerName = dto.OwnerName;
            existing.OwnerContact = dto.OwnerContact;
            existing.SeatingCapacity = dto.SeatingCapacity;
            existing.ACAvailable = dto.ACAvailable;

            if (dto.Image != null)
            {
                existing.ImagePath =
                    await _cloudinaryService.UploadImageAsync(dto.Image);
            }

            await _repository.UpdateAsync(id, existing);

            return Ok(new
            {
                message = "Rental Car Updated Successfully",
                data = existing
            });
        }

        // DELETE: api/RentalCar/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _repository.GetByIdAsync(id);

            if (existing == null)
                return NotFound();

            await _repository.DeleteAsync(id);

            return Ok(new
            {
                message = "Rental Car Deleted Successfully"
            });
        }
    }
}