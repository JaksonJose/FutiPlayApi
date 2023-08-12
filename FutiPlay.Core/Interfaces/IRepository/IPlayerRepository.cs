
using FutiPlay.Core.Models;
using FutiPlay.Core.Response;

namespace FutiPlay.Core.Interfaces.IRepository
{
    public interface IPlayerRepository
    {
        /// <summary>
        /// Fetch all players
        /// </summary>
        /// <returns>Response object with
        /// the players list and messages</returns>
        public Task<PlayerResponse> FetchAllTeamMemberAsync();
    }
}
