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
            // יצירת טוקן חדש רק אם יש טוקן קיים (אם זה נחוץ להוסיף טוקן חדש)
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                try
                {
                    // חילוץ המידע מהטוקן הקיים
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:Key")));
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var principal = ValidateToken(token, secretKey);

                    if (principal != null)
                    {
                        // חילוץ נתוני Claims מהטוקן המאומת
                        var name = principal.Identity.Name; // שם המשתמש
                        var role = principal.FindFirst(ClaimTypes.Role)?.Value; // תפקיד

                        // יצירת טוקן חדש (אם נדרש)
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, name),
                            new Claim(ClaimTypes.Role, role)
                        };

                        var newToken = new JwtSecurityToken(
                            issuer: _configuration.GetValue<string>("JWT:Issuer"),
                            audience: _configuration.GetValue<string>("JWT:Audience"),
                            claims: claims,
                            expires: DateTime.Now.AddMinutes(5), // יצירת טוקן חדש שיתפוגג אחרי 5 דקות
                            signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
                        );

                        var newTokenString = tokenHandler.WriteToken(newToken);

                        // שולח את הטוקן החדש בתגובה כ-Authorization header
                        context.Response.Headers["Authorization"] = "Bearer " + newTokenString;
                    }
                }
                catch (Exception ex)
                {
                    // טיפול בשגיאות במקרה של טוקן לא חוקי
                    context.Response.StatusCode = 401; // Unauthorized
                    await context.Response.WriteAsync($"Token validation failed: {ex.Message}");
                    return;
                }
            }

            // המשך לעיבוד הבא
            await _next(context);
        }

        private ClaimsPrincipal ValidateToken(string token, SymmetricSecurityKey secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                // הגדרת אובייקט החתימה לאימות הטוקן
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = secretKey,
                    ValidIssuer = _configuration.GetValue<string>("JWT:Issuer"),
                    ValidAudience = _configuration.GetValue<string>("JWT:Audience")
                };

                // אימות הטוקן
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

                // אם הטוקן הוא מסוג JwtSecurityToken, אז הוא ה-Token הרגיל
                if (validatedToken is JwtSecurityToken jwtToken && jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return principal;
                }

                // אם הטוקן לא תקין
                throw new SecurityTokenException("Invalid token.");
            }
            catch (Exception ex)
            {
                // טיפול בשגיאות בעת אימות הטוקן
                throw new SecurityTokenException("Token validation failed: " + ex.Message);
            }
        }
    }
}
