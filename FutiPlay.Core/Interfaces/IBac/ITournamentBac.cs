using FutiPlay.Core.Response;

namespace FutiPlay.Core.Interfaces.IBac
{
	public interface ITournamentBac
	{
		/// <summary>
		/// Fetches tournament by request
		/// </summary>
		/// <returns>Response object</returns>
		public Task<TournamentResponse> FetchTournamentByRequestAsync();
	}
}
