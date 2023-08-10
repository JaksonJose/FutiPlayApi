using Dapper;
using FutiPlay.Core.Interfaces.IRepository;
using FutiPlay.Core.Models;
using FutiPlay.Core.Response;
using Microsoft.Extensions.Logging;
using System.Data;

namespace FutiPlay.Data.Repositories
{
	public class TournamentRepository : ITournamentRepository
	{
		public List<Tournament> FetchAllTournamentsAsync()
		{
			List<Tournament> tournaments = new List<Tournament>
			{
				new Tournament()
				{
					Id = 1,
					Name = "Rankeada",
					StartDate = "10/02/2024",
					EndDate = "10/03/2024",
					Description = " ",
					Status = "ATIVO",
				},
				new Tournament()
				{
					Id = 2,
					Name = "Rankeada 2",
					StartDate = "10/02/2024",
					EndDate = "10/03/2024",
					Description = " ",
					Status = "CANCELADO",
				}
			};

			return tournaments;
		}
	}
}
