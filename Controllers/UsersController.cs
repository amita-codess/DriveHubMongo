using DriveHubMongo.DTO;
using DriveHubMongo.Model;
using DriveHubMongo.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DriveHubMongo.Services;

[Route("api/[controller]")]
[ApiController]
public class UsersController(
    IUserRepository userRepository,
    IEmailService emailService) : ControllerBase
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IEmailService _emailService = emailService;

    private static List<OtpStore> otpStores = new();
    // ---------------- Register ----------------

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

    // ---------------- Get All Users ----------------

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return Ok(users);
    }

    // ---------------- Login ----------------

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

    // ---------------- Forgot Password ----------------

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
    {
        var user = await _userRepository.GetUserByEmailAsync(dto.Email);

        if (user == null)
        {
            return BadRequest(new
            {
                Message = "Email not found."
            });
        }

        Random random = new Random();

        string otp = random.Next(100000, 999999).ToString();

        otpStores.RemoveAll(x => x.Email == dto.Email);

        otpStores.Add(new OtpStore
        {
            Email = dto.Email,
            Otp = otp,
            ExpiryTime = DateTime.Now.AddMinutes(5)
        });

        await _emailService.SendOtpEmail(dto.Email, otp);

        return Ok(new
        {
            Message = "OTP sent to your email successfully."
        });
    }   // <-- ForgotPassword ends here

    // ---------------- Verify OTP ----------------

    [HttpPost("verify-otp")]
    public IActionResult VerifyOtp(VerifyOtpDto dto)
    {
        var otp = otpStores.FirstOrDefault(x =>
            x.Email == dto.Email &&
            x.Otp == dto.Otp);

        if (otp == null)
        {
            return BadRequest(new
            {
                Message = "Invalid OTP"
            });
        }

        if (otp.ExpiryTime < DateTime.Now)
        {
            return BadRequest(new
            {
                Message = "OTP Expired"
            });
        }

        return Ok(new
        {
            Message = "OTP Verified"
        });
    }

    // ---------------- Reset Password ----------------

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
    {
        var user = await _userRepository.GetUserByEmailAsync(dto.Email);

        if (user == null)
        {
            return BadRequest(new
            {
                Message = "User not found"
            });
        }

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

        await _userRepository.UpdateUserAsync(user);

        otpStores.RemoveAll(x => x.Email == dto.Email);

        return Ok(new
        {
            Message = "Password changed successfully"
        });
    } // ResetPassword ends

}  // UsersController ends 