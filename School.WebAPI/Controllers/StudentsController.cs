using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.WebAPI.Helpers.Interfaces;
using School.WebAPI.Models;
using School.WebAPI.Models.JsonWrappers;

namespace School.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private ICsvFileParser _csvFileParser;

        public StudentsController(ICsvFileParser csvFileParser)
        {
            _csvFileParser = csvFileParser;
        }

        [HttpPost]
        [Route("createFromBody")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CreateStudentsFromBody([FromBody] StudentListWrapper studentsWrapper)
        {
            List<Student> students = studentsWrapper.Students;
            return Ok();
        }

        [HttpPost]
        [Route("createFromFile")]
        public async Task<IActionResult> CreateStudensFromFile([FromForm] IFormFile formFile)
        {
            if (formFile != null && formFile.Length > 0)
            {
                //var filePath = Path.GetTempFileName();
                string filePath = Path.GetTempFileName();

                using var stream = System.IO.File.Create(filePath);
                await formFile.CopyToAsync(stream);
                stream.Seek(0, SeekOrigin.Begin);

                List<Student> students = await _csvFileParser.ParseCsvPublicSchoolFile(stream);
            }

            return Ok();
        }
    }
}
