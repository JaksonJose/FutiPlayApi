using FutiPlay.Core.Models;
using FutiPlay.Core.Response;

namespace FutiPlay.Core.Interfaces.IBac
{
	public interface ITournamentBac
	{
		public Task<TournamentResponse> FetchTournamentByRequestAsync();
	}
}
