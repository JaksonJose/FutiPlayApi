
using FutiPlay.Core.Response;

namespace FutiPlay.Core.Interfaces.IRepository
{
	public interface ITournamentRepository
	{
        /// <summary>
        /// Fetches Tournaments by request
        /// </summary>
        /// <returns>Response object</returns>
        public Task<TournamentResponse> FetchTournamentByRequestAsync();
	}
}
