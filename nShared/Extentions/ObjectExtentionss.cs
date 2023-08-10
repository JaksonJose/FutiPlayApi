
namespace xShared.Extentions
{
    public static class ObjectExtentionss
    {
        /// <summary>   opposite of isnull. </summary>
        /// <param name="editValue">    the object. </param>
        /// <returns>   boolean. </returns>
        public static bool IsNotNull(this object editValue)
        {
            bool ret;
            ret = editValue != null && editValue != DBNull.Value;
            return ret;
        }

        /// <summary>   checks if an unknown type contains a null. </summary>
        /// <param name="editValue">    the object. </param>
        /// <returns>   boolean. </returns>
        public static bool IsNull(this object editValue)
        {
            return editValue.IsNotNull() == false;
        }
    }
}
