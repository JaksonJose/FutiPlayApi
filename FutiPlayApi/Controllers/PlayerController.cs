using FutiPlay.Api.Controllers.Base;
using FutiPlay.Core.Interfaces.IBac;
using FutiPlay.Core.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutiPlay.Api.Controllers
{
    [Route("[controller]")]
    public class PlayerController : BaseController
    {
        private readonly ILogger<PlayerController> _logger;
        private readonly IPlayerBac _playerBac;
        public PlayerController(IPlayerBac playerBac, ILogger<PlayerController> logger)
        {
            _logger = logger;
            _playerBac = playerBac;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> FetchAllPlayersAsync()
        {
            PlayerResponse response = await _playerBac.FetchAllPlayersAsync();
            if (response.HasSystemErrorMessages)
                BadRequest(response);           

            return Ok(response);
        }
    }
}
