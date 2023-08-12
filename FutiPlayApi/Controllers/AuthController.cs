using FutiPlay.Api.Controllers.Base;
using FutiPlay.Core.Identity.Models;
using FutiPlay.Core.Interfaces.IBac;
using FutiPlay.Core.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutiPlay.Api.Controllers
{
    [Route("[controller]")]
    public class AuthController : BaseController
    {
        private readonly IIdentityBac _identityBac;

        public AuthController(IIdentityBac identityBac)
        {
            _identityBac = identityBac;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> SignInAsync([FromBody] UserLogin user)
        {
            UserSimpleResponse response = await _identityBac.AuthUserBac(user);
            if (response.HasValidationMessages)
            {
                BadRequest(response.Messages);
            }

            return Ok(response);
        }
    }
}
