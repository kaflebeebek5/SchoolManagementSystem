using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Configurations
{
    /// <summary>
    /// Base api controller. All controller should extendds this controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/Auth/[controller]")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class BaseAPIController:Controller
    {
    }
}
