
namespace FutiPlay.Core.Models
{
    public class Match : BaseModel
    {    
        public int TournamentId { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public int HomeTeamScore { get; set; }

        public int AwayTeamScore { get; set; }

        public DateTime GameDatatime { get; set; }

        public string Location { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public int StatisticId { get; set; }
    }
}
