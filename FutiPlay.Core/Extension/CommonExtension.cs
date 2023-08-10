
using xShared.Responses;
using xShared.Extentions;

namespace FutiPlay.Core.Extension
{
    public static class CommonExtension
    {
        /// <summary>
        /// Verify if operation has any errors or not validated data
        /// </summary>
        /// <param name="response">Response containing the messages to avaliate</param>
        /// <returns>true or false</returns>
        public static bool InError(this BaseResponse response)
        {
            bool inError = false;
            bool hasErrorMessages = response.HasErrorMessages
                                    || response.HasSystemErrorMessages
                                    || response.HasValidationMessages;

            if (response.IsNull() || hasErrorMessages)
            {
                inError = true;
            }

            return inError;
        }
    }
}
