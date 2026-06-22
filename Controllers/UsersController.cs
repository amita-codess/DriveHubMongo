using DriveHubBackend.Data;
using DriveHubBackend.DTO;
using DriveHubBackend.Model;
using DriveHubBackend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserRepository userRepository) : ControllerBase
{
    private readonly IUserRepository _userRepository = userRepository;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingUser =
            await _userRepository.GetUserByEmailAsync(dto.Email);

        if (existingUser != null)
        {
            return BadRequest("Email already exists");
        }

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            MobileNumber = dto.MobileNumber,
            PasswordHash =
                BCrypt.Net.BCrypt.HashPassword(dto.Password),

            Role = "User"
        };

        await _userRepository.AddUserAsync(user);
        await _userRepository.SaveChangesAsync();

        return Ok(new
        {
            Message = "Registration Successful"
        });

    }


    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return Ok(users);
    }
}
