using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo1.Controllers
{
    [Route("api/department")]
    [Route("api/v{veresion:apiVersion}/department")]
    [ApiController]
    [ApiVersion("1.0")]
    public class DepartmentController : ControllerBase
    {
        [HttpGet]
        [ApiVersion("1.0", Deprecated = true)]
        [Obsolete("This endpoint is deprecated. Use v2 instead.")]
        public IActionResult GetDepartment()
        {
            var departments = new List<string>
            {
                "HR",
                "IT",
                "Finance",
                "Marketing"
            };
            return Ok(departments);
        }

        [HttpGet]
        [ApiVersion("2.0")]
        public IActionResult GetDepartment2()
        {
            return Ok("This is Version 2 of GetDepartments");
        }


        [HttpGet("{id}")]
        [ApiVersion("2.0")]
        public IActionResult GetDepartment(string id)
        {

            return Ok("Received Id = " + id);
        }
    }
}
