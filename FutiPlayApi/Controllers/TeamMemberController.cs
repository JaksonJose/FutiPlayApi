using FutiPlay.Api.Controllers.Base;
using FutiPlay.Core.Extension;
using FutiPlay.Core.Interfaces.IBac;
using FutiPlay.Core.Response;
using Microsoft.AspNetCore.Mvc;

namespace FutiPlay.Api.Controllers
{
    public class TeamMemberController : BaseController
    {
        private readonly ILogger<TeamMemberController> _logger;
        private readonly ITeamMemberBac _playerBac;

        public TeamMemberController(ITeamMemberBac playerBac, ILogger<TeamMemberController> logger)
        {
            _logger = logger;
            _playerBac = playerBac;
        }

        [HttpGet]
        public async Task<IActionResult> FetchTeamMemberByRequestAsync()
        {
            TeamMemberResponse response = await _playerBac.FetchTeamMemberByRequestAsync();
            if (response.InError())
                BadRequest(response);           

            return Ok(response);
        }
    }
}
