using Microsoft.AspNetCore.Mvc;
using FutiPlay.Api.Controllers.Base;
using FutiPlay.Core.Interfaces.IBac;
using Microsoft.AspNetCore.Authorization;
using FutiPlay.Core.Response;
using FutiPlay.Core.Models;
using xShared.Request;

namespace FutiPlay.Api.Controllers
{
	public class TournamentController : BaseController
	{
		private readonly ILogger<TournamentController> _logger;
		private readonly ITournamentBac _tournamentBac;
		public TournamentController(ITournamentBac tournamentBac, ILogger<TournamentController> logger)
		{
			_logger = logger;
			_tournamentBac = tournamentBac;
		}

		[HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> FetchAllTournamentsAsync([FromQuery] InquiryRequest request)
		{
			TournamentResponse tournament = await _tournamentBac.FetchTournamentByRequestAsync();

			return Ok(tournament);
		}
	}
}
