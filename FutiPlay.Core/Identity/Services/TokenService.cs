
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

            // Create the claim of all roles associated to the user.
            IEnumerable<Claim> roleClaim = auth.UserRoles!.Select(role => new Claim(ClaimTypes.Role, role));         
            byte[] cryptographedKey = Encoding.ASCII.GetBytes(jwtKey.SecretKey);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, auth.Email), // User.Identity.Name
                }.Union(roleClaim)),
                Expires = DateTime.UtcNow.AddHours(6), //token lifetime
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(cryptographedKey), SecurityAlgorithms.HmacSha256Signature),
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
