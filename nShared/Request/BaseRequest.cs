using System.Globalization;
using xShared.Attributes;
using xShared.Interfaces;
using xShared.Models;

namespace xShared.Request
{
    /// <summary>
    /// This BaseRequest should be used as the base class for all Request type classes.
    /// It provides a set of standard properties every request should include.
    /// This can be used within a tightly coupled solution or a loosely coupled solution.
    /// </summary>
    public class BaseRequest : IBaseRequest
    {
        /// <summary>
        /// The user making the request or System Default
        /// </summary>
        public string User { get; set; } = "System Default";

        /// <summary>
        /// Used to set the Culture Info instance for this request.
        /// Usually this is the Culture Info instance the user wishes to use.
        /// This defaults to CultureInfo.CurrentCulture
        /// </summary>
        public CultureInfo CultureInfo { get; set; } = CultureInfo.CurrentCulture;

        /// <summary>
        /// Every request that is created is given a new <see cref="SequentialDateGuid" />, unique identifier.
        /// </summary>
        [SwaggerPropertyIgnoreAttribute]
        public string RequestUniqueId { get; } = SequentialDateGuid.NewSequentialDateGuid().ToString();

        /// <summary>
        /// Where applicable it might be helpful to know the HTTP context in which the request was created.
        /// <see cref="Microsoft.AspNetCore.Http.HttpMethods" /> for a list of valid values and the various comparison methods available.
        /// Note this will be null by default.
        /// </summary>
        [SwaggerPropertyIgnoreAttribute]
        public string HttpMethod { get; set; }
    }
}
