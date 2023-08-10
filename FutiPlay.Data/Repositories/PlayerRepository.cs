
using Dapper;
using FutiPlay.Core.Interfaces.IRepository;
using FutiPlay.Core.Models;
using FutiPlay.Core.Response;
using Microsoft.Extensions.Logging;
using System.Data;

namespace FutiPlay.Data.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger<PlayerRepository> _logger;

        public PlayerRepository(IDbConnection dbConnection, ILogger<PlayerRepository> logger)
        {
            _dbConnection = dbConnection;
            _logger = logger;
        }

        /// <summary>
        /// Fetch all players
        /// </summary>
        /// <returns>Response object with
        /// the players list and messages</returns>
        public async Task<PlayerResponse> FetchAllPlayersAsync()
        {
            PlayerResponse response = new();

            try
            {
                IEnumerable<Player> result = await _dbConnection.QueryAsync<Player>("SELECT * FROM Player");
                List<Player>  playerList = result.ToList();

                response.ResponseData.AddRange(playerList);                 
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception was throw at PlayersRepository.FetchAllPlayersAsync() :: {ex.Message}");

                response.AddExceptionMessage("500", ex.Message);
            }

            return response;
        }
    }
}
