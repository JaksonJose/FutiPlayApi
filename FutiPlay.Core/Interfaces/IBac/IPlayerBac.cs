using FutiPlay.Core.Models;
using FutiPlay.Core.Response;

namespace FutiPlay.Core.Interfaces.IBac
{
    public interface IPlayerBac
    {
        /// <summary>
        /// Fetches all players
        /// </summary>
        /// <returns>Response object with players list
        /// and messages</returns>
        public Task<PlayerResponse> FetchAllTeamMemberAsync();
    }
}
