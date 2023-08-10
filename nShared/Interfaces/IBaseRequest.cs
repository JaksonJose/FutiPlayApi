
namespace xShared.Interfaces
{
    /// <summary>
    /// The definition of a Base Request
    /// </summary>
    public interface IBaseRequest
    {
        /// <summary>
        /// The name of the HTTP Method
        /// </summary>
        string HttpMethod { get; set; }

        /// <summary>
        /// Unique identifier for the request used for tracing.
        /// </summary>
        string RequestUniqueId { get; }

        /// <summary>
        /// The user identifier making the request.
        /// </summary>
        string User { get; set; }
    }
}
