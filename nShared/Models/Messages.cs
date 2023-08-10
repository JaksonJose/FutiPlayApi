using xShared.Enum;
using xShared.Interfaces;

namespace xShared.Models
{
    public class Message : IMessage
    {
        /// <summary>
        /// Default empty constructor.
        /// </summary>
        public Message()
        { }

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="type"></param>
        /// <param name="code"></param>
        /// <param name="text"></param>
        public Message(MessageTypeEnum type, string? code, string? text = null)
        {
            Type = type;
            Code = code;
            Text = text;
        }

        /// <summary>
        /// The type of message based on the enumeration MessageType
        /// </summary>
        public MessageTypeEnum Type { get; set; } = MessageTypeEnum.None;

        /// <summary>
        /// A unique message code to be used to easily locate in the source code where a message was created but also to support internationalization.
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// The message text associated with the code.
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// Return a stringified version of this class
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Type:{Type}, Code:{Code}, Message:{Text}";
        }

        /// <summary>
        /// Convenience method for creating validation message.
        /// Message type if automatically set to <see cref="MessageTypeEnum.Validation"/>
        /// </summary>
        /// <param name="code"></param>
        /// <param name="text"></param>
        public static Message CreateValidationMessage(string? code, string? text = null)
        {
            return new Message(MessageTypeEnum.Validation, code, text);
        }

        /// <summary>
        /// Convenience method for creating error message.
        /// Message type if automatically set to <see cref="MessageTypeEnum.Error"/>
        /// </summary>
        /// <param name="code"></param>
        /// <param name="text"></param>
        public static Message CreateErrorMessage(string? code, string? text = null)
        {
            return new Message(MessageTypeEnum.Error, code, text);
        }

        /// <summary>
        /// Convenience method for creating warning message.
        /// Message type if automatically set to <see cref="MessageTypeEnum.Warning"/>
        /// </summary>
        /// <param name="code"></param>
        /// <param name="text"></param>
        public static Message CreateWarningMessage(string? code, string? text = null)
        {
            return new Message(MessageTypeEnum.Warning, code, text);
        }

        /// <summary>
        /// Convenience method for creating info message.
        /// Message type if automatically set to <see cref="MessageTypeEnum.Info"/>
        /// </summary>
        /// <param name="code"></param>
        /// <param name="text"></param>
        public static Message CreateInfoMessage(string? code, string? text = null)
        {
            return new Message(MessageTypeEnum.Info, code, text);
        }
    }
}
