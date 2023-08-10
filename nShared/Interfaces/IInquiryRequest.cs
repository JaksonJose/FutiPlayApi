using xShared.Interfaces;
using xShared.Request;

namespace xShared.Interfaces
{
    public interface IInquiryRequest : IBaseRequest
    {
        /// <summary>
        /// Used to indicate if various summary/total related properties should be included in the response.
        /// </summary>
        bool IncludeSummaryInformation { get; set; }

        /// <summary>
        /// The requested page number
        /// </summary>
        int PageNumber { get; set; }

        /// <summary>
        /// The requested page size in rows.
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// Collection of sort expressions.
        /// </summary>
        IEnumerable<SortExpression> SortExpressions { get; }

        /// <summary>
        /// Short-hand version of sort expressions.
        /// </summary>
        List<string> ShorthandSortExpressions { get; set; }

        /// <summary>
        /// Helper method.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="columnAlias"></param>
        /// <param name="tableAlias"></param>
        /// <returns></returns>
        InquiryRequest AddSortExpressionAscending(string propertyName, string? columnAlias = null, string? tableAlias = null);

        /// <summary>
        /// Helper method
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="columnAlias"></param>
        /// <param name="tableAlias"></param>
        /// <returns></returns>
        InquiryRequest AddSortExpressionDescending(string propertyName, string? columnAlias = null, string? tableAlias = null);
    }
}
