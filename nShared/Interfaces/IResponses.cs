using xShared.Models;
using xShared.Responses;

namespace xShared.Interfaces
{
    /// <summary>
    /// Definition of a base response instance.
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        /// Does this have any messages
        /// </summary>
        public bool HasAnyMessages { get; }
        /// <summary>
        /// Does this have any error messages.
        /// </summary>
        public bool HasErrorMessages { get; }
        /// <summary>
        /// Does this have any error messages.
        /// </summary>
        public bool HasInformationMessages { get; }
        /// <summary>
        /// Does this have any information messages.
        /// </summary>
        public bool HasSystemErrorMessages { get; }
        /// <summary>
        /// Does this have any system error messages.
        /// </summary>
        public bool HasValidationMessages { get; }
        /// <summary>
        /// Does this have any validation messages.
        /// </summary>
        public bool HasWarningMessages { get; }
        /// <summary>
        /// Does this have any warning messages.
        /// </summary>
        public List<Message> Messages { get; set; }

        /// <summary>
        /// Helper method to add error messages
        /// </summary>
        /// <param name="code"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public BaseResponse AddErrorMessage(string? code, string? text = null);

        /// <summary>
        /// Helper method to add Exception messages
        /// </summary>
        /// <param name="code"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public BaseResponse AddExceptionMessage(string? code, string? text);

        /// <summary>
        /// Helper method to add information type messages
        /// </summary>
        /// <param name="code"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public BaseResponse AddInfoMessage(string? code, string? text = null);

        /// <summary>
        /// Helper method to add validation type messages.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public BaseResponse AddValidationMessage(string? code, string? text = null);

        /// <summary>
        /// Helper method to add warning type messages
        /// </summary>
        /// <param name="code"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public BaseResponse AddWarningMessage(string? code, string? text = null);

        /// <summary>
        /// Helper method used to merge the Internal Response type instance into this Response.
        /// </summary>
        /// <param name="response"></param>
        public void Merge(InternalResponse response);
    }
}
