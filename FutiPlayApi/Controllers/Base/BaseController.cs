using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutiPlay.Api.Controllers.Base
{
    [ApiController]
    [Authorize]
    [Route("api")]
    public class BaseController : ControllerBase
    {
    }
}
