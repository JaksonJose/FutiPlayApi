namespace FutiPlay.Core.Models
{
	public class Tournament : BaseModel
	{
		public string Name { get; set; } = string.Empty;

		public string StartDate { get; set; } = string.Empty;

		public string EndDate { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public string Status { get; set; } = string.Empty;
    }
}
