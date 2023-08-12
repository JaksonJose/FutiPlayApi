
using System.Text.Json.Serialization;

namespace FutiPlay.Core.Models
{
    public class BaseModel
    {
        /// <summary>
        /// Consider global time zone
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Consider global time zone
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// User that registered or modified the row
        /// </summary>
        [JsonIgnore]
        public string ModifiedBy { get; set; } = string.Empty;
    }
}
