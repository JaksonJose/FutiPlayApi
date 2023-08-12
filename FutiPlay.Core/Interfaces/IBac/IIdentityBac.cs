
using FutiPlay.Core.Identity.Models;
using FutiPlay.Core.Response;

namespace FutiPlay.Core.Interfaces.IBac
{
    public interface IIdentityBac
    {
        /// <summary>
        /// Authenticate user bac
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns>User response data</returns>
        public Task<UserSimpleResponse> AuthUserBac(UserLogin userLogin);
    }
}
