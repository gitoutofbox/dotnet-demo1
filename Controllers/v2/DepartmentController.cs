using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo1.Controllers
{
    [Route("api/v{version:apiVersion}/department")]
    [ApiController]
    [Asp.Versioning.ApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    public class DepartmentController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetDepartmentList()
        {
            return Ok("Department list");
        }
    }
}
