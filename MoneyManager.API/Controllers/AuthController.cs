using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MoneyManager.Application.DTOs.Auth;
using MoneyManager.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MoneyManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
private readonly UserManager<ApplicationUser> _userManager;
private readonly IConfiguration _configuration;


public AuthController(
    UserManager<ApplicationUser> userManager,
    IConfiguration configuration)
{
    _userManager = userManager;
    _configuration = configuration;
}

[HttpPost("register")]
public async Task<IActionResult> Register(RegisterDto dto)
{
    var user = new ApplicationUser
    {
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        Email = dto.Email,
        UserName = dto.Email
    };

    var result = await _userManager.CreateAsync(
        user,
        dto.Password);

    if (!result.Succeeded)
    {
        return BadRequest(result.Errors);
    }

    return Ok("Utilisateur créé avec succès.");
}

[HttpPost("login")]
public async Task<IActionResult> Login(LoginDto dto)
{
    var user = await _userManager.FindByEmailAsync(dto.Email);

    if (user == null)
    {
        return Unauthorized("Email ou mot de passe incorrect.");
    }

    var isPasswordValid =
        await _userManager.CheckPasswordAsync(user, dto.Password);

    if (!isPasswordValid)
    {
        return Unauthorized("Email ou mot de passe incorrect.");
    }

    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Email, user.Email!),
        new Claim(ClaimTypes.Name, user.UserName!)
    };

    var key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(
            _configuration["Jwt:Key"]!));

    var credentials = new SigningCredentials(
        key,
        SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: _configuration["Jwt:Issuer"],
        audience: _configuration["Jwt:Audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(
            Convert.ToDouble(
                _configuration["Jwt:DurationInMinutes"])),
        signingCredentials: credentials);

    return Ok(new
    {
        token = new JwtSecurityTokenHandler()
            .WriteToken(token)
    });
}


}
