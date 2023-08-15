using FutiPlay.Core.Response;

namespace FutiPlay.Core.Interfaces.IBac
{
    public interface ITeamMemberBac
    {
        /// <summary>
        /// Fetches all players
        /// </summary>
        /// <returns>Response object with players list
        /// and messages</returns>
        public Task<TeamMemberResponse> FetchTeamMemberByRequestAsync();
    }
}
