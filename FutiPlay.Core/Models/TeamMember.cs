
namespace FutiPlay.Core.Models
{
    public class TeamMember : BaseModel
    {
        public string Name { get; set; } = string.Empty;

        public string Position { get; set; } = string.Empty;

        public int TeamId { get; set; }
    }
}
