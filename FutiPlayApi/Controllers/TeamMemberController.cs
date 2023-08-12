using FutiPlay.Api.Controllers.Base;
using FutiPlay.Core.Interfaces.IBac;
using FutiPlay.Core.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutiPlay.Api.Controllers
{
    public class TeamMemberController : BaseController
    {
        private readonly ILogger<TeamMemberController> _logger;
        private readonly IPlayerBac _playerBac;
        public TeamMemberController(IPlayerBac playerBac, ILogger<TeamMemberController> logger)
        {
            _logger = logger;
            _playerBac = playerBac;
        }

        [HttpGet]
        public async Task<IActionResult> FetchAllTeamMemberAsync()
        {
            PlayerResponse response = await _playerBac.FetchAllTeamMemberAsync();
            if (response.HasSystemErrorMessages)
                BadRequest(response);           

            return Ok(response);
        }
    }
}
