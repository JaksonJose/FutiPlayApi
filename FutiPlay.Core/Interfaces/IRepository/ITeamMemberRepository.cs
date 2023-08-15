
using FutiPlay.Core.Models;
using FutiPlay.Core.Response;

namespace FutiPlay.Core.Interfaces.IRepository
{
    public interface ITeamMemberRepository
    {
        /// <summary>
        /// Fetch the Team Members by request
        /// </summary>
        /// <returns>Team Member response object</returns>
        public Task<TeamMemberResponse> FetchTeamMemberByRequestAsync();
    }
}
