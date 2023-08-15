using FutiPlay.Core.Interfaces.IBac;
using FutiPlay.Core.Interfaces.IRepository;
using FutiPlay.Core.Response;

namespace FutiPlay.Core.Bac
{
    public class TeamMemberBac : ITeamMemberBac
    {
        private readonly ITeamMemberRepository _playerRepository;

        public TeamMemberBac(ITeamMemberRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        /// <summary>
        /// Fetches all players
        /// </summary>
        /// <returns>Response object with players list
        /// and messages</returns>
        public async Task<TeamMemberResponse> FetchTeamMemberByRequestAsync()
        {
            TeamMemberResponse response = await _playerRepository.FetchTeamMemberByRequestAsync();

            return response;
        }
    }
}
