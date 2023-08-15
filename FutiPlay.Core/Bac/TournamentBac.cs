using FutiPlay.Core.Interfaces.IRepository;
using FutiPlay.Core.Response;
using FutiPlay.Core.Interfaces.IBac;
using FutiPlay.Core.Models;
using System;

namespace FutiPlay.Core.Bac
{
	public class TournamentBac : ITournamentBac
	{
		private readonly ITournamentRepository _tournamentRepository;

		public TournamentBac(ITournamentRepository tournamentRepository)
		{
			_tournamentRepository = tournamentRepository;

		}

		public async Task<TournamentResponse> FetchTournamentByRequestAsync()
		{
			TournamentResponse tournaments = await _tournamentRepository.FetchTournamentByRequestAsync();

			return tournaments;

        }
	}
}
