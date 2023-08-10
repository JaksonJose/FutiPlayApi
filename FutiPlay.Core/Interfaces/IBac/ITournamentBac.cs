using FutiPlay.Core.Models;


namespace FutiPlay.Core.Interfaces.IBac
{
	public interface ITournamentBac
	{
		public List<Tournament> FetchAllTournamentsAsync();
	}
}
