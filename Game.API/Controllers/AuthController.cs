using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Game.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetToken([FromRoute] int id)
        {
            string role;
            if (id == 1)
            {
                role = "admin";
            }
            else
            {
                role = "user";
            }
            var token = GenerateJwtToken(role);

            return Ok(token);
        }



        public string GenerateJwtToken(string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("096c0a72c31f9a2d65126d8e8a401a2ab2f2e21d0a282a6ffe6642bbef65ffd9");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "GiangNguyen"),
                new Claim(ClaimTypes.Role, role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.UtcNow.AddDays(7),

                // sign the token with secret
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "GiangNguyen",
                Audience = "Audience"

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
    
}
