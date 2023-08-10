
namespace xShared.Enum
{
    public enum MessageTypeEnum
    {
        // None, default
        None,

        // The Message is Informational.
        Info,

        // The Message is a Warning.
        Warning,

        // The Message is an Error.
        Error,

        // The Message is Fatal. Is also considered a System error.
        Fatal,

        // The Message is an Exception. Is also considered a System error.
        Exception,

        // The Message is related to validation.
        Validation
    }
}
