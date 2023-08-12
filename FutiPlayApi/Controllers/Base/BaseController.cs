using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutiPlay.Api.Controllers.Base
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
    }
}
