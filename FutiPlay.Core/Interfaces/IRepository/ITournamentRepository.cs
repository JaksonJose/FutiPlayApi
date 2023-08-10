using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FutiPlay.Core.Models;
using FutiPlay.Core.Response;

namespace FutiPlay.Core.Interfaces.IRepository
{
	public interface ITournamentRepository
	{

		public List<Tournament> FetchAllTournamentsAsync();
	}
}
