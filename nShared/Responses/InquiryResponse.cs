
using xShared.Interfaces;

namespace xShared.Responses
{
    /// <summary>
    /// This is the other side of the <see cref="Requests.InquiryRequest" /> class and wraps the content of the response data
    /// in a collection along with other meta-information.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InquiryResponse<T> : BaseResponse, IInquiryResponse<T>
    {
        /// <summary>
        /// Default empty constructor
        /// </summary>
        public InquiryResponse() : base()
        {
        }

        /// <summary>
        /// Typical constructor for creating an "Ok" response with a collection of objects.
        /// </summary>
        /// <param name="items"></param>
        public InquiryResponse(List<T> items) : base()
        {
            this.ResponseData = items;
        }

        /// <summary>
        /// Simple constructor to add a single item to the return collection.
        /// </summary>
        /// <param name="item"></param>
        public InquiryResponse(T item) : base()
        {
            if (item != null)
            {
                this.ResponseData.Add(item);
            }
        }

        /// <summary>
        /// Construct from a response and copy over matching properties.
        /// </summary>
        /// <param name="request"></param>
        public InquiryResponse(IInquiryRequest request)
        {
            CopyFromRequest(request);
        }

        /// <summary>
        /// This property will be populated by the BAR with the total number of rows available based on criteria.<br/>
        /// So the BAR would populate this property accordingly executing the query, without paging
        /// constraints, in order to determine total items/rows based on search criteria.<br/>
        /// With paging you don't return all the rows, so this number gives the client a feel for how many are available and helps the client 
        /// understand the number of pages to plan for.
        /// </summary>
        public int AvailableTotalItems { get; set; }

        /// <summary>
        /// This is used in the context of paging and is used to indicate the number of rows that should be included in a single page of results.
        /// It is returned here as a copy of what was passed in with the request.
        /// </summary>
        public int RequestedPageSize { get; set; } = 100;

        /// <summary>
        /// This is used in the context of paging and is used to indicate which page was returned based on the PageSize.
        /// </summary>
        public int RequestedPageNumber { get; set; } = 0;

        /// <summary>
        /// The return values based on the request criteria.
        /// </summary>
        public List<T> ResponseData { get; set; } = new List<T>();

        /// <summary>
        /// Convenience property returning the number of rows found in the ResponseData list.
        /// </summary>
        public int ResponseDataCount
        {
            get
            {
                return (ResponseData != null) ? ResponseData.Count : 0;
            }
        }

        /// <summary>
        /// Convenience method to copy over properties relevant to the response from the request.
        /// </summary>
        /// <param name="request"></param>
        public void CopyFromRequest(IInquiryRequest request)
        {
            base.CopyFromRequest(request);
            RequestedPageNumber = request.PageNumber;
            RequestedPageSize = request.PageSize;
        }
    }
}