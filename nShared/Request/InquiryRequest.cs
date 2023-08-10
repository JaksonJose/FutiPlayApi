
using System.Text;
using xShared.Attributes;

namespace xShared.Request
{
    /// <summary>
    /// InquiryRequest is used in the context of requesting data using a variety of criteria.   
    /// Support for paging through the PageSize and PageNumber properties
    /// </summary>
    public class InquiryRequest
    {
        /// <summary>
        /// This is used in the context of paging and is used to indicate the number of rows that should be included in a single page of results.
        /// This property defaults to 20.
        /// </summary>
        public int PageSize { get; set; } = 20;

        /// <summary>
        /// This is used in the context of paging and is used to indicate which page should be returned based on the PageSize.
        /// Note from the users point of view PageNumber starts at 1, but from the database point of view PageNumber starts at zero.
        /// This property defaults to 1.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Support for non-pages type scrolling, AKA Virtual Scrolling based on an offset and then the page size.
        /// If this is greater than zero than Virtual Scrolling will be used by repositories.
        /// </summary>
        public int VirtualScrollingOffset { get; set; } = 0;

        /// <summary>
        /// This is similar to <see cref="IncludeAvailableTotalItems"/> property in that it costs extra to query and provide back to the consumer/client/browser
        /// summary type data.  Totals, aggregations, high-level calculations may not need to be run with every request back to the server so with this
        /// property the consumer/client/browser can specify when to return this type of summary information in the response.
        /// Note this property defaults to false.
        /// </summary>
        public bool IncludeSummaryInformation { get; set; } = false;

        /// <summary>
        /// Provides a sorted collection, by priority, enumerable instance of Sort Expressions.
        /// This uses the collection from ShorthandSortExpressions as the source.
        /// </summary>
        [SwaggerPropertyIgnore]
        public IEnumerable<SortExpression> SortExpressions
        {
            get
            {
                List<SortExpression> sortExpressions = new();

                int priority = 1;
                foreach (string orderBy in ShorthandSortExpressions)
                {
                    sortExpressions.Add(SortExpression.Parse(orderBy, priority));
                    priority++;
                }

                sortExpressions.Sort((x, y) => x.Priority.CompareTo(y.Priority));

                return sortExpressions;
            }
        }

        /// <summary>
        /// This property provides a "shorthand" means of creating SortExpression instances.<br/>
        /// The short hand format is as follows:<br/><ul>
        /// <li> Character position 1 is either a plus-sign, +, or a minus-sign, -, indicating ascending or descending sort direction.  </li>
        /// <li> Next is the property name upon which the sort should occur.  </li>
        /// </ul>
        /// Examples:<br/><ul>
        ///  <li>+FirstName  sort FirstName property ascending. </li>
        ///  <li>+Active sort by Active property ascending. </li>
        ///  <li>-AddDate sort by AddDate property descending. </li>
        /// </ul>
        /// The priority will be set based on the order in which they entries are added.  
        /// First entry added will have priority 1, second entry added will have priority 2 etc.
        /// </summary>
        public List<string> ShorthandSortExpressions { get; set; } = new();

        /// <summary>
        /// Convenience method for adding sort expressions.
        /// The priority will be determined based on the order in which the entries are added.  
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="tableAlias"></param>
        /// <param name="columnAlias"></param>
        /// <returns></returns>
        public InquiryRequest AddSortExpressionAscending(string propertyName, string tableAlias = "", string columnAlias = "")
        {
            ShorthandSortExpressions.Add("+" + propertyName + "-" + tableAlias + "-" + columnAlias);
            return this;
        }

        /// <summary>
        /// Convenience method for adding sort expression.
        /// The priority will be determined based on the order in which the entries are added.  
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="tableAlias"></param>
        /// <param name="columnAlias"></param>
        /// <returns></returns>
        public InquiryRequest AddSortExpressionDescending(string propertyName, string tableAlias = "", string columnAlias = "")
        {
            ShorthandSortExpressions.Add("-" + propertyName + "-" + tableAlias + "-" + columnAlias);
            return this;
        }

        /// <summary>
        /// Convenience method for adding sort expression.
        /// The priority will be determined based on the order in which the entries are added.  
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="propertyValues"></param>
        /// <param name="tableAlias"></param>
        /// <param name="columnAlias"></param>
        /// <returns></returns>
        public InquiryRequest AddSortExpressionAscending(string propertyName, List<string> propertyValues, string tableAlias = "", string columnAlias = "")
        {
            string joinValues = string.Join("~", propertyValues);
            ShorthandSortExpressions.Add($"+{propertyName}-{joinValues}-{tableAlias}-{columnAlias}");
            return this;
        }

        /// <summary>
        /// Convenience method for adding sort expression.
        /// The priority will be determined based on the order in which the entries are added.  
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="propertyValues"></param>
        /// <param name="tableAlias"></param>
        /// <param name="columnAlias"></param>
        /// <returns></returns>
        public InquiryRequest AddSortExpressionDescending(string propertyName, List<string> propertyValues, string tableAlias = "", string columnAlias = "")
        {
            string joinValues = string.Join("~", propertyValues);
            ShorthandSortExpressions.Add($"-{propertyName}-{joinValues}-{tableAlias}-{columnAlias}");
            return this;
        }

        /// <summary>
        /// Helper method to generate the order by clause.
        /// Note the returned string does not include "ORDER BY", just the properties/columns to sort by and their direction.
        /// </summary>
        /// <param name="tablePrefix"></param>
        /// <returns></returns>
        public string BuildSortExpressionForSql(string? tablePrefix = null)
        {
            StringBuilder sb = new();

            foreach (SortExpression exp in SortExpressions)
            {
                sb.Append((tablePrefix == null) ?
                    exp.ToSqlFragment().Trim() + ", " :
                    exp.ToSqlFragmentWithPrefix(tablePrefix).Trim() + ", ");
            }

            //remove last comma and space
            sb.Length -= 2;
            return sb.ToString() + " ";
        }
    }
}
