
namespace FutiPlay.Core.Models
{
    public class BaseModel
    {
        /// <summary>
        /// Consider global time zone
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Consider global time zone
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// User that registered or modified the row
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;
    }
}
