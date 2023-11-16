using Microsoft.AspNetCore.Mvc;

namespace ContactManagementAPI.Controllers.v2;

[Route("api/v{version:apiVersion}/contactApi")]
[ApiVersion("2.0")]
[ApiController]
public class ContactApiController : ControllerBase
{

    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }
}
