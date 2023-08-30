
using FutiPlay.Core.Identity.Models;
using FutiPlay.Core.Identity.Services;
using FutiPlay.Core.Interfaces.IBac;
using FutiPlay.Core.Models;
using FutiPlay.Core.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace FutiPlay.Core.Identity.Bac
{
    public class IdentityBac : IIdentityBac
    {
        private readonly JWTKey _keyJWT;
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityBac(UserManager<ApplicationUser> userManager, IOptions<JWTKey> keyJWT)
        {
            _userManager = userManager;
            _keyJWT = keyJWT.Value;
        }

        /// <summary>
        /// Authenticate user bac
        /// </summary>
        /// <param name="userLogin">Email e password</param>
        /// <returns>User response data</returns>
        public async Task<UserSimpleResponse> AuthUserBac(UserLogin userLogin)
        {
            UserSimpleResponse response = new();

            bool isPassword = false;

            // Find the user by identity framework
            ApplicationUser? appUser = await _userManager.FindByEmailAsync(userLogin.Email);

            if (appUser != null)
            {
                // Verifies if password is correct
                isPassword = await _userManager.CheckPasswordAsync(appUser, userLogin.Password);
            }

            if (isPassword && appUser != null)
            {
                response.Model = appUser;

                response = await CreateAndStoreToken(userLogin, response);

                return response;
            }

            response.AddValidationMessage("", "User or password did not match");

            return response;
        }

        /// <summary>
        /// Create, Update Token
        /// </summary>
        /// <param name="userLogin">Email and password</param>
        /// <param name="response">Response object</param>
        /// <returns>User response object</returns>
        private async Task<UserSimpleResponse> CreateAndStoreToken(UserLogin userLogin, UserSimpleResponse response)
        {
            string token = userLogin.GenerateToken(_keyJWT);
            ApplicationUser user = response.Model;

            // Store the user token in the database
            IdentityResult result = await _userManager.SetAuthenticationTokenAsync(user, "FutiPlayApi", "JwtToken", token);

            if (!result.Succeeded)
            {
                response.AddErrorMessage("001", "Couldn't store the jwt token");

                return response;
            }

            response.Token = token;

            return response;
        }
    }
}
