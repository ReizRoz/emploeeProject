using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectEmploee.Api.Midllewars
{    public class tokenMidllewer
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public tokenMidllewer(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                try
                {

                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:Key")));
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var principal = ValidateToken(token, secretKey);

                    if (principal != null)
                    {
                        var name = principal.Identity.Name; 
                        var role = principal.FindFirst(ClaimTypes.Role)?.Value; 

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, name),
                            new Claim(ClaimTypes.Role, role)
                        };

                        var newToken = new JwtSecurityToken(
                            issuer: _configuration.GetValue<string>("JWT:Issuer"),
                            audience: _configuration.GetValue<string>("JWT:Audience"),
                            claims: claims,
                            expires: DateTime.Now.AddMinutes(5), 
                            signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
                        );

                        var newTokenString = tokenHandler.WriteToken(newToken);

                        context.Response.Headers["Authorization"] = "Bearer " + newTokenString;
                    }
                }
                catch (Exception ex)
                {
           
                    context.Response.StatusCode = 401; 
                    await context.Response.WriteAsync($"Token validation failed: {ex.Message}");
                    return;
                }
            }

            await _next(context);
        }

        private ClaimsPrincipal ValidateToken(string token, SymmetricSecurityKey secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = secretKey,
                    ValidIssuer = _configuration.GetValue<string>("JWT:Issuer"),
                    ValidAudience = _configuration.GetValue<string>("JWT:Audience")
                };

                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);


                if (validatedToken is JwtSecurityToken jwtToken && jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return principal;
                }

                throw new SecurityTokenException("Invalid token.");
            }
            catch (Exception ex)
            {

                throw new SecurityTokenException("Token validation failed: " + ex.Message);
            }
        }
    }
}
