using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth.API.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Auth.API.Controller;

[Route("/auth")]
[ApiController]
public class AuthController : Microsoft.AspNetCore.Mvc.Controller
{
    private UserDbContext _userDbContext;
    private IPasswordHasher<UserModel> _passwordHasher;
    private IConfiguration _config;

    public AuthController(UserDbContext userDbContext, IPasswordHasher<UserModel> passwordHasher, IConfiguration config)
    {
        _userDbContext=  userDbContext;
        _passwordHasher = passwordHasher;
        _config = config;
        ;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetALlUsers()
    {
        //List<UserModel> allUsers = await _userDbContext.Users.ToListAsync();
        return Ok("hello");
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (await _userDbContext.Users.AnyAsync(u => u.Email == registerDto.email))
            return BadRequest("Email is already registered.");

        var newUser = new UserModel
        {
            Email = registerDto.email,
            Password = registerDto.password,
            Role = "user"
        };
        
        await _userDbContext.Users.AddAsync(newUser);
        await _userDbContext.SaveChangesAsync();
        return Ok(newUser);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userDbContext.Users.FirstOrDefaultAsync(u => u.Email == loginDto.email);

        if (user == null)
        {
            return Unauthorized("Invalid credentials - unregistered email");
        }

        if (loginDto.password != user.Password)
            return Unauthorized("Invalid credentials - wrong password.");

        var token = GenerateJwtToken(user);

        return Ok(token);
    }
    // GET
    public string GenerateJwtToken(UserModel user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"] ?? throw new InvalidOperationException("Can not find Jwt key"));

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),

            Expires = DateTime.UtcNow.AddDays(7),

            // sign the token with secret
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"]

        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}