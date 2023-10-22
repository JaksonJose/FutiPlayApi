
using FutiPlay.Core.Identity.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FutiPlay.Core.Identity.Models
{
    public class UserLogin
    {
        /// <summary>
        /// Gets or sets the Email
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [JsonIgnore]
        public IList<string>? UserRoles { get; set; }
    }
}
