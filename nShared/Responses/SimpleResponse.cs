
namespace xShared.Responses
{
    /// <summary>
    /// A simple response for generic use.
    /// </summary>
    public class SimpleResponse : BaseResponse
    {
    }

    /// <summary>
    /// A simple typed response containing a Model property of the specified type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SimpleResponse<T> : BaseResponse
    {
        public required T Model { get; set; }
    }
}
