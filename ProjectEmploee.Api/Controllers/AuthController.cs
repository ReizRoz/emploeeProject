using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectEmploee.Core.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ProjectEmploee.Core.Enum;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using ProjectEmploee.Core.Entities;

namespace ProjectEmploee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SystemAdministrator, Admin, Emploee")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userServices;

        public AuthController(IConfiguration configuration, IUserService userServices)
        {
            _configuration = configuration;
            _userServices = userServices;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login loginModel)
        {
            var login = await _userServices.GetByNameAsync(loginModel.UserName);
            if (login != null && login.Password == loginModel.Password)
            {
                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, login.Name),
                new Claim(ClaimTypes.Role, login.Role.ToString())
            };

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:Key")));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: _configuration.GetValue<string>("JWT:Issuer"),
                    audience: _configuration.GetValue<string>("JWT:Audience"),
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString });
            }
            return Unauthorized();
        }
    }
}
