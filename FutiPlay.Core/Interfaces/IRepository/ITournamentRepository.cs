
using FutiPlay.Core.Models;
using FutiPlay.Core.Response;
using xShared.Request;
using xShared.Responses;

namespace FutiPlay.Core.Interfaces.IRepository
{
	public interface ITournamentRepository
	{
        /// <summary>
        /// Fetches Tournaments by request
        /// </summary>
        /// <returns>Response object</returns>
        public Task<TournamentResponse> FetchTournamentByRequestAsync();

        /// <summary>
        /// Insert Tournament by request
        /// </summary>
        /// <param name="request">Request containing the model to be inserted</param>
        /// <returns>Response of inserted data</returns>
        public Task<ModelOperationResponse> InsertTournamentByRequestAsync(ModelOperationRequest<Tournament> request);
    }
}
