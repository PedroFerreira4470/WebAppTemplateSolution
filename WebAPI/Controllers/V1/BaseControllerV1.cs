using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class BaseControllerV1 : BaseController
    {
    }
}
