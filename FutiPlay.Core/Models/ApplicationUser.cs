
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace FutiPlay.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// User first Name
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// User Last Name
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// The responsible that created the register
        /// </summary>
        [JsonIgnore]
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// The responsible that updated the register
        /// </summary>
        [JsonIgnore]
        public string UpdatedBY { get; set; } = string.Empty;

        /// <summary>
        /// Date and time that the register was updated
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Date and time that the register was created
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset CreatedAt { get; set; }
    }
}
