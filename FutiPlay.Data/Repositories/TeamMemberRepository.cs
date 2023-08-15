
using Dapper;
using FutiPlay.Core.Interfaces.IRepository;
using FutiPlay.Core.Models;
using FutiPlay.Core.Response;
using Microsoft.Extensions.Logging;
using System.Data;
using static Dapper.SqlBuilder;

namespace FutiPlay.Data.Repositories
{
    public class TeamMemberRepository : ITeamMemberRepository
    {
        #region SQL

        private static readonly string SelectTeamMember = "SELECT * FROM TeamMember";

        #endregion


        private readonly IDbConnection _dbConnection;
        private readonly ILogger<TeamMemberRepository> _logger;

        public TeamMemberRepository(IDbConnection dbConnection, ILogger<TeamMemberRepository> logger)
        {
            _dbConnection = dbConnection;
            _logger = logger;
        }

        /// <summary>
        /// Fetch the Team Members by request
        /// </summary>
        /// <returns>Team Member response object</returns>
        public async Task<TeamMemberResponse> FetchTeamMemberByRequestAsync()
        {
            TeamMemberResponse response = new();

            //Build the SQL
            SqlBuilder builder = new();
            string querySql = string.Join(' ', SelectTeamMember);
            Template sqlTemplate = builder.AddTemplate(querySql);
            string sql = sqlTemplate.RawSql;

            try
            {
                IEnumerable<TeamMember> result = await _dbConnection.QueryAsync<TeamMember>(sql);
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
