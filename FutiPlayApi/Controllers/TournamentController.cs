using Microsoft.AspNetCore.Mvc;
using FutiPlay.Api.Controllers.Base;
using FutiPlay.Core.Models;

using FutiPlay.Core.Interfaces.IBac;
using Microsoft.AspNetCore.Authorization;

namespace FutiPlay.Api.Controllers
{
	[Route("[controller]")]
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
        public IActionResult FetchAllTournamentsAsync()
		{
			var tournament = _tournamentBac.FetchAllTournamentsAsync();

			return Ok(tournament);
		}
	}
}
