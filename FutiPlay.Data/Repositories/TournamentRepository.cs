using Dapper;
using FutiPlay.Core.Interfaces.IRepository;
using FutiPlay.Core.Models;
using FutiPlay.Core.Response;
using Microsoft.Extensions.Logging;
using System.Data;
using static Dapper.SqlBuilder;

namespace FutiPlay.Data.Repositories
{
	public class TournamentRepository : ITournamentRepository
	{
        private readonly string SelectTournament = "SELECT * FROM Tournament";

        private readonly IDbConnection _dbConnection;
        private readonly ILogger<TournamentRepository> _logger;

        public TournamentRepository(IDbConnection dbConnection, ILogger<TournamentRepository> logger)
        {
            _dbConnection = dbConnection;
            _logger = logger;
        }

        /// <summary>
        /// Fetches the Tournaments by request
        /// </summary>
        /// <returns>Tournament response object</returns>
        public async Task<TournamentResponse> FetchTournamentByRequestAsync()
		{
            TournamentResponse response = new();

            //Build the SQL
            SqlBuilder builder = new();
            string querySql = string.Join(' ', SelectTournament);
            Template sqlTemplate = builder.AddTemplate(querySql);
            string sql = sqlTemplate.RawSql;

            try
            {
                IEnumerable<Tournament> result = await _dbConnection.QueryAsync<Tournament>(sql);
                List<Tournament> playerList = result.ToList();

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
