
namespace xShared.Responses
{
    /// <summary>
    /// This class provides a simplified concrete version of <see cref="BaseResponse"/> which can be leveraged within a project but never returned to a 
    /// network-based client/consumer.<br/>
    /// Use this response type between layers, BAC=>BAR, BAC=>BAC and sometimes Controller=>BAC as needed where a regular InquiryResponse 
    /// or ModelOperationResponse does not make sense.<br/>
    /// </summary>
    public class InternalResponse : BaseResponse
    {
    }
}
