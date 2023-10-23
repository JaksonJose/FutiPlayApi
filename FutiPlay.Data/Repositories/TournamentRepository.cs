using Dapper;
using FutiPlay.Core.Interfaces.IRepository;
using FutiPlay.Core.Models;
using FutiPlay.Core.Response;
using Microsoft.Extensions.Logging;
using System.Data;
using xShared.Extentions;
using xShared.Request;
using xShared.Responses;
using static Dapper.SqlBuilder;
using static Dapper.SqlMapper;
using static System.Net.Mime.MediaTypeNames;

namespace FutiPlay.Data.Repositories
{
	public class TournamentRepository : ITournamentRepository
	{
        private readonly string SelectTournament = "SELECT * FROM Tournament t";
        private readonly string InsertTournament = "INSERT INTO Tournament (Name, StartDate, EndDate, Description, Status)";
        private readonly string InsertValues = "VALUES (@Name, @StartDate, @EndDate, @Description, @Status)";

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
        /// <returns>Response object</returns>
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
                response.ResponseData.AddRange(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception was throw at FetchTournamentByRequestAsync.FetchAllPlayersAsync() :: {ex.Message}");

                response.AddExceptionMessage("500", ex.Message);
            }

            return response;
		}

        /// <summary>
        /// Insert Tournament by request
        /// </summary>
        /// <param name="request">Request containing the model to be inserted</param>
        /// <returns>Response of inserted data</returns>
        public async Task<ModelOperationResponse> InsertTournamentByRequestAsync(ModelOperationRequest<Tournament> request)
        {
            ModelOperationResponse response = new();

            try
            {
                SqlBuilder builder = new();
                string querySql = string.Join(' ', InsertTournament, InsertValues);

                int result = await _dbConnection.ExecuteAsync(querySql, request.Model);

                if(result == 0)
                {
                    response.AddErrorMessage("Tournament was not added");
                }

                response.AddInfoMessage("Tournament was successfully added"); 
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception was throw at InsertTournamentByRequestAsync.FetchAllPlayersAsync() :: {ex.Message}");
                response.AddExceptionMessage("500", ex.Message);
            }

            return response;
        }
    }
}
