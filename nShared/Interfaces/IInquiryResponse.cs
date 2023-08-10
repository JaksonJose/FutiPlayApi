
using xShared.Interfaces;

namespace xShared.Interfaces
{
    public interface IInquiryResponse<T>
    {
        /// <summary>
        /// The originally requested page number found in the request.
        /// </summary>
        int RequestedPageNumber { get; set; }

        /// <summary>
        /// The originally requested page size found in the request.
        /// </summary>
        int RequestedPageSize { get; set; }

        /// <summary>
        /// The collection of response data instances
        /// </summary>
        List<T> ResponseData { get; set; }

        /// <summary>
        /// <see cref="InquiryResponse{T}.AvailableTotalItems" />
        /// </summary>
        int AvailableTotalItems { get; set; }

        /// <summary>
        /// Convenience method to copy over properties relevant to the response from the request.
        /// </summary>
        /// <param name="request"></param>
        void CopyFromRequest(IInquiryRequest request);
    }
}
