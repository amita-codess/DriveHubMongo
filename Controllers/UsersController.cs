using DriveHubMongo.DTO;
using DriveHubMongo.Model;
using DriveHubMongo.Repositories;
using Microsoft.AspNetCore.Mvc;

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

        var existingUser = await _userRepository.GetUserByEmailAsync(dto.Email);

        if (existingUser != null)
        {
            return BadRequest("Email already exists");
        }

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            MobileNumber = dto.MobileNumber,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = "User"
        };

        await _userRepository.AddUserAsync(user);

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

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _userRepository.GetUserByEmailAsync(dto.Email);

        if (user == null)
        {
            return BadRequest("Invalid Email");
        }

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(
            dto.Password,
            user.PasswordHash);

        if (!isPasswordValid)
        {
            return BadRequest("Invalid Password");
        }

        return Ok(new
        {
            Message = "Login Successful",
            UserId = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        });
    }
}