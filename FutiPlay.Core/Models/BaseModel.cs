
using System.Text.Json.Serialization;

namespace FutiPlay.Core.Models
{
    public class BaseModel
    {
        public int Id { get; set; }

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
        public string UpdatedBy { get; set; } = string.Empty;

        /// <summary>
        /// User that created the register
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Set date and time to CreateAt Property
        /// </summary>
        public void SetCreateAtDateAndTime()
        {
            CreatedAt = DateTimeOffset.Now;
        }

        /// <summary>
        /// Set date and time to UpadtedAt property
        /// </summary>
        public void SetUpdatedAtDateAndTime()
        {
            UpdatedAt = DateTimeOffset.Now;
        }
    }
}
