
using Dapper;
using FutiPlay.Core.Interfaces.IRepository;
using FutiPlay.Core.Models;
using FutiPlay.Core.Response;
using Microsoft.Extensions.Logging;
using System.Data;

namespace FutiPlay.Data.Repositories
{
    public class TeamMemberRepository : IPlayerRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger<TeamMemberRepository> _logger;

        public TeamMemberRepository(IDbConnection dbConnection, ILogger<TeamMemberRepository> logger)
        {
            _dbConnection = dbConnection;
            _logger = logger;
        }

        /// <summary>
        /// Fetch all players
        /// </summary>
        /// <returns>Response object with
        /// the players list and messages</returns>
        public async Task<PlayerResponse> FetchAllTeamMemberAsync()
        {
            PlayerResponse response = new();

            try
            {
                IEnumerable<TeamMember> result = await _dbConnection.QueryAsync<TeamMember>("SELECT * FROM TeamMember");
                List<TeamMember>  playerList = result.ToList();

                response.ResponseData.AddRange(playerList);                 
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception was throw at TeamMemberRepository.FetchAllPlayersAsync() :: {ex.Message}");

                response.AddExceptionMessage("500", ex.Message);
            }

            return response;
        }
    }
}
