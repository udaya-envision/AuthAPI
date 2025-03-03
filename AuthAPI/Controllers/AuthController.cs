using AuthAPI.Models;
using AuthAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // Simulated user check (Replace this with database check)
        if (request.Username == "admin" && request.Password == "password123")
        {
            var token = GenerateJwtToken(request.Username);
            return Ok(new { token });
        }

        return Unauthorized("Invalid credentials");
    }

    [Authorize] // Ensures authentication is required
    [HttpGet("debug-claims")]
    public IActionResult DebugClaims()
    {
        var user = HttpContext.User;

        var claims = user.Claims.Select(c => new { c.Type, c.Value }).ToList();

        return Ok(claims);
    }

    [HttpGet("test-auth")]
    public IActionResult TestAuth()
    {
        var userClaims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
        return Ok(userClaims);
    }

    private string GenerateJwtToken(string username)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, username),
        new Claim("Permissions", "Administer") 
    };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}
