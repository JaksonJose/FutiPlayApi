
using FutiPlay.Core.Models;
using xShared.Responses;

namespace FutiPlay.Core.Response
{
    public class UserSimpleResponse : SimpleResponse<ApplicationUser>
    {
        public string Token { get; set; }
    }
}
