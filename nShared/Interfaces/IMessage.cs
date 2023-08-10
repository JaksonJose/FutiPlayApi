

using xShared.Enum;

namespace xShared.Interfaces
{
    /// <summary>
    /// Interface for defining a message
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// The Message Code
        /// </summary>
        string Code { get; set; }

        /// <summary>
        /// The Message Text
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// The Message Type.
        /// </summary>
        MessageTypeEnum Type { get; set; }
    }
}
