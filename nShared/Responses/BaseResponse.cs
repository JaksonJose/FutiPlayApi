
using xShared.Enum;
using xShared.Interfaces;
using xShared.Models;
using System.Text;

namespace xShared.Responses
{
    /// <summary>
    /// Base class for all response-types which includes various properties for evaluating the response and the results of the request made
    /// to the server or from one method to another.<br/>
    /// </summary>
    public abstract class BaseResponse : IResponse
    {
        /// <summary>
        /// A collection of messages related to the response.
        /// </summary>
        public List<Message> Messages { get; set; } = new List<Message>();

        /// <summary>
        /// Unique identifier assigned to every request copied here.
        /// </summary>
        public string? RequestUniqueId { get; set; }

        /// <summary>
        /// Helper method used to add a Exception type message to the underlying Messages collection.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public BaseResponse AddExceptionMessage(string? code, string? text = null)
        {
            Messages.Add(new Message(MessageTypeEnum.Exception, code, text));
            return this;
        }

        /// <summary>
        /// Helper method used to add a validation type message to the underlying Messages collection.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public BaseResponse AddValidationMessage(string? code, string? text = null)
        {
            Messages.Add(new Message(MessageTypeEnum.Validation, code, text));
            return this;
        }

        /// <summary>
        /// Helper method used to add a error type message to the underlying Messages collection.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public BaseResponse AddErrorMessage(string? code, string? text = null)
        {
            Messages.Add(new Message(MessageTypeEnum.Error, code, text));
            return this;
        }

        /// <summary>
        /// Helper method used to add a warning type message to the underlying Messages collection.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public BaseResponse AddWarningMessage(string? code, string? text = null)
        {
            Messages.Add(new Message(MessageTypeEnum.Warning, code, text));
            return this;
        }

        /// <summary>
        /// Helper method used to add a info type message to the underlying Messages collection.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public BaseResponse AddInfoMessage(string? code, string? text = null)
        {
            Messages.Add(new Message(MessageTypeEnum.Info, code, text));
            return this;
        }

        /// <summary>
        /// Simple property reflecting if the message collection has any messages.
        /// </summary>
        public bool HasAnyMessages
        { get { return Messages.Count > 0; } }

        /// <summary>
        /// If the Messages collection has any Fatal or Exception type messages return true.
        /// </summary>
        /// <returns></returns>
        public bool HasSystemErrorMessages
        { get { return HasMessageType(MessageTypeEnum.Fatal) || HasMessageType(MessageTypeEnum.Exception); } }

        /// <summary>
        /// If the Messages collection has any Information type messages return true.
        /// </summary>
        /// <returns></returns>
        public bool HasInformationMessages
        { get { return HasMessageType(MessageTypeEnum.Info); } }

        /// <summary>
        /// If the Messages collection has any Validation messages return true.
        /// </summary>
        /// <returns></returns>
        public bool HasValidationMessages
        { get { return HasMessageType(MessageTypeEnum.Validation); } }

        /// <summary>
        /// If the Messages collection has any Error type messages return true.
        /// </summary>
        /// <returns></returns>
        public bool HasErrorMessages
        {
            get { return HasMessageType(MessageTypeEnum.Error); }
        }

        /// <summary>
        /// If the Messages collection has any Warning type messages return true.
        /// </summary>
        /// <returns></returns>
        public bool HasWarningMessages
        {
            get { return HasMessageType(MessageTypeEnum.Warning); }
        }

        private bool HasMessageType(MessageTypeEnum messageType)
        {
            return Messages.Any(item => item.Type == messageType);
        }

        /// <summary>
        /// Most often used for debuging of logging, this method returs a formated string of all the messages within this response.
        /// </summary>
        /// <param name="includeNewLine">If true then each message includes a newline character</param>
        /// <returns>Formated string.</returns>
        public string MessagesToString(bool includeNewLine = false)
        {
            StringBuilder sb = new();
            foreach (var msg in Messages)
            {
                if (includeNewLine)
                {
                    sb.AppendLine($"code:{msg.Code}, text:{msg.Text}");
                }
                else
                {
                    sb.Append($"code:{msg.Code}, text:{msg.Text}");
                }
            }
            if (sb.Length < 1)
                return "No Messages";

            return sb.ToString();
        }

        /// <summary>
        /// Merge the results together being careful not to overlay OperationSuccess when it's already false and
        /// SystemError when it's true.
        /// As well the message collections will be merged.
        /// </summary>
        /// <param name="response"></param>
        public virtual void Merge(InternalResponse response)
        {
            this.Messages.AddRange(response.Messages);
        }

        /// <summary>
        /// Helper method used to merge the contents of one response into another.
        /// </summary>
        /// <param name="response"></param>
        public virtual void Merge(IResponse response)
        {
            this.Messages.AddRange(response.Messages);
        }

        /// <summary>
        /// Helper method used to copy the contents of one request into another.
        /// </summary>
        /// <param name="request"></param>
        public virtual void CopyFromRequest(IBaseRequest request)
        {
            RequestUniqueId = request.RequestUniqueId;
        }
    }
}
