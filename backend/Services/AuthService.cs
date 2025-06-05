using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Models;
using backend.Services.IServices;
using Microsoft.IdentityModel.Tokens;

namespace backend.Services
{
    public class AuthService : IAuthService
    {
        private JwtToken _jwtToken;
        public AuthService(
                JwtToken jwtToken
            )
        {
            _jwtToken = jwtToken;
        }

        public string GenerateToken(User user)
        {
            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, "ExpensesProject"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email)
                };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtToken.SecretKey));
            var creds = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenObj = new JwtSecurityToken(
                   issuer: _jwtToken.Issuer,
                   audience: _jwtToken.Audience,
                   claims: claims,
                   expires: DateTime.Now.AddMinutes(_jwtToken.ExpiryMinutes),
                   signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenObj);
        }
    }
}
