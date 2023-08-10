
using FutiPlay.Core.Identity.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FutiPlay.Core.Identity.Services
{
    public static class TokenService
    {
        /// <summary>
        /// Generates Token
        /// </summary>
        /// <param name="auth">login info</param>
        /// <param name="jwtKey">JWT Key for security</param>
        /// <returns>Token generated</returns>
        public static string GenerateToken(this UserLogin auth, JWTKey jwtKey)
        {
            JwtSecurityTokenHandler tokenHandler = new();

            // Get Criptographed key
            byte[] criptographedKey = Encoding.ASCII.GetBytes(jwtKey.SecretKey);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, auth.Email), // User.Identity.Name
                    //TODO: Implement user roles
                }),
                Expires = DateTime.UtcNow.AddHours(6), //token lifetime
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(criptographedKey), SecurityAlgorithms.HmacSha256Signature),
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
