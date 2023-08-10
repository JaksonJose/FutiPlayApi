
namespace xShared.Request
{
    public class SortExpression
    {
        /// <summary>   Values that represent directions. </summary>
        /// <remarks>   Wednesday, January 11, 2017. </remarks>
        public enum Direction
        {
            /// <summary>   An constant representing the Ascending option. </summary>
            ASC,

            /// <summary>   An constant representing the Description option. </summary>
            DESC
        }

        /// <summary>   Gets or sets the name of the property. </summary>
        /// <value> The name of the property. </value>
        public string PropertyName { get; set; }

        /// <summary> Gets or Sets the name of a list of property </summary>
        /// <value> The list of the property. </value>
        //public List<SortCaseExpression> SortCaseExpressions { get; set; }
        public List<string> PropertyValues { get; set; } = new();

        /// <summary>   Gets or sets the sort direction. </summary>
        /// <value> The sort direction. </value>
        public Direction SortDirection { get; set; }

        /// <summary>   Gets or sets the priority. </summary>
        /// <value> The priority. </value>
        public int Priority { get; set; }

        /// <summary> Gets or sets the table alias. </summary>
        public string? TableAlias { get; set; }

        /// <summary> Gets or sets the column alias. </summary>
        public string? ColumnAlias { get; set; }

        /// <summary>
        /// Default empty constructor
        /// </summary>
        public SortExpression()
        {
        }

        /// <summary>
        /// Simple constructor setting the priority for the sort expression.
        /// </summary>
        /// <param name="priority"></param>
        public SortExpression(int priority)
        {
            this.Priority = priority;
        }

        /// <summary>
        /// Simple constructor without table and column aliases
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="sortDirection"></param>
        /// <param name="priority"></param>
        public SortExpression(string propName, Direction sortDirection, int priority)
        {
            this.PropertyName = propName;
            this.SortDirection = sortDirection;
            this.Priority = priority;
        }

        /// <summary>
        /// Full sized constructor
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="tableAlias"></param>
        /// <param name="columnAlias"></param>
        /// <param name="sortDirection"></param>
        /// <param name="priority"></param>
        public SortExpression(string propName, string tableAlias, string columnAlias, Direction sortDirection, int priority)
        {
            this.PropertyName = propName;
            this.TableAlias = tableAlias;
            this.ColumnAlias = columnAlias;
            this.SortDirection = sortDirection;
            this.Priority = priority;
        }

        /// <summary>
        /// Returns a SQL formated version of this Sort Expression
        /// </summary>
        /// <returns></returns>
        public string ToSqlFragment()
        {
            return $" {PropertyName} {SortDirection} ";
        }

        /// <summary>
        /// Returns a SQL formated version of this Sort Expression with the property prefixed based on the parameter value.
        /// </summary>
        /// <returns></returns>
        public string ToSqlFragmentWithPrefix(string prefix)
        {
            return $" {prefix}.{PropertyName} {SortDirection} ";
        }

        /// <summary>
        /// Give a format of +MyPropertyName or -MyPropertyName or MyPropertyName
        /// Create a new instance 
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public static SortExpression Parse(string sortBy, int priority = 1)
        {
            SortExpression expression = new()
            {
                Priority = priority
            };

            if (sortBy.StartsWith('-'))
                expression.SortDirection = Direction.DESC;
            else if (sortBy.StartsWith('+'))
                expression.SortDirection = Direction.ASC;

            if (sortBy.Contains('~'))
            {
                string properties = sortBy[1..];
                string[] splitProperties = properties.Split("-");
                string[] splitPropertyValues = splitProperties[1].Split("~");

                expression.PropertyName = splitProperties[0];
                expression.PropertyValues.AddRange(splitPropertyValues);
                expression.TableAlias = string.IsNullOrEmpty(splitProperties[2]) ? null : splitProperties[2];
                expression.ColumnAlias = string.IsNullOrEmpty(splitProperties[3]) ? null : splitProperties[3];
            }
            else
            {
                string properties = sortBy[1..];
                string[] splitProperties = properties.Split("-");
                expression.PropertyName = splitProperties[0];
                expression.TableAlias = string.IsNullOrEmpty(splitProperties[1]) ? null : splitProperties[1];
                expression.ColumnAlias = string.IsNullOrEmpty(splitProperties[2]) ? null : splitProperties[2];
            }

            return expression;
        }
    }
}
