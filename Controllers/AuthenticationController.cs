using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Demo1.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IConfiguration _configuration;
        public class LoginRequest
        {
            public string? Username { get; set; }
            public string? Password { get; set; }
        }
        public class User
        {
            public int Id { get; set; }
            public string Email { get; set; }
            public string Name { get; set; }
            public string Country { get; set; } = string.Empty;
            public User(int id, string email, string name, string country)
            {
                Id = id;
                Email = email;
                Name = name;
                Country = country;
            }
        }

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public ActionResult<string> Login(LoginRequest request)
        {
            var userInfo = ValidUserInfo(request.Username, request.Password);
            if (userInfo == null)
            {
                return Unauthorized();
            }
            var secretKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration["Authentication:Secret"]));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();
            claims.Add(new Claim("sub", userInfo.Id.ToString()));
            claims.Add(new Claim("email", userInfo.Email));
            claims.Add(new Claim("name", userInfo.Name));
            claims.Add(new Claim("country", userInfo.Country));
            
            var jwtToken = new JwtSecurityToken(
                issuer: _configuration["Authentication:Issuer"],
                audience: _configuration["Authentication:Audiences"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signingCredentials
            );

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return Ok(tokenToReturn);
        }

        private User ValidUserInfo(string? username, string? password)
        {
            // In real world, you will validate the user info from database
            return new User(1, "john@gmail.com", "John", "India");
        }
    }
}
