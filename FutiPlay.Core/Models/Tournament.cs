namespace FutiPlay.Core.Models
{
	public class Tournament : BaseModel
	{
		public string Name { get; set; } = string.Empty;

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public string Description { get; set; } = string.Empty;

		public string Status { get; set; } = string.Empty;
    }
}
