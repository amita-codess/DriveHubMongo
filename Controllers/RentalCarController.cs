using DriveHubMongo.DTO;
using DriveHubMongo.Model;
using DriveHubMongo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DriveHubMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalCarController : ControllerBase
    {
        private readonly IRentalCarRepository _repository;
        private readonly IWebHostEnvironment _environment;


        public RentalCarController(
            IRentalCarRepository repository,
            IWebHostEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cars = await _repository.GetAllAsync();
            return Ok(cars);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var car = await _repository.GetByIdAsync(id);

            if (car == null)
                return NotFound();

            return Ok(car);
        }



        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var cars = await _repository.GetByUserIdAsync(userId);

            return Ok(cars);
        }



        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] AddRentalCarDto dto)
        {
            string imagePath = "/uploads/default-rentalcar.webp";


            if (dto.Image != null)
            {
                var folderPath = Path.Combine(
                    _environment.WebRootPath,
                    "uploads"
                );


                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);



                var fileName = Guid.NewGuid().ToString()
                    + Path.GetExtension(dto.Image.FileName);


                var filePath = Path.Combine(
                    folderPath,
                    fileName
                );


                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }


                imagePath = "/uploads/" + fileName;
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



            await _repository.UpdateAsync(
                id,
                existing
            );


            return Ok(new
            {
                message = "Rental Car Updated Successfully"
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
                message = "Rental Car Deleted Successfully"
            });
        }
    }
}
