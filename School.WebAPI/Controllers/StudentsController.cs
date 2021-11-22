using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.WebAPI.Models;
using School.WebAPI.Models.JsonWrappers;

namespace School.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpPost]
        [Route("createFromBody")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CreateStudentFromBody([FromBody] UserListWrapper users)
        {
            return Ok();
        }
    }
}
