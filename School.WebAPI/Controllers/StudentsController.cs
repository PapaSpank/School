using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.WebAPI.BLL;
using School.WebAPI.Helpers.Interfaces;
using School.WebAPI.Models;
using School.WebAPI.Models.Internal;
using School.WebAPI.Models.JsonWrappers;

namespace School.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ICsvFileParser _csvFileParser;
        private readonly IStudentBLL _studentBLL;

        public StudentsController(ICsvFileParser csvFileParser,
            IStudentBLL studentBLL)
        {
            _csvFileParser = csvFileParser;
            _studentBLL = studentBLL;
        }

        [HttpPost]
        [Route("createFromBodyPublic")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CreateStudentsFromBody([FromBody] StudentListWrapper studentsWrapper)
        {
            List<Student> students = studentsWrapper.Students;
            return Ok();
        }

        [HttpPost]
        [Route("createFromFilePublic")]
        public async Task<IActionResult> CreateStudentsFromFile([FromForm] IFormFile formFile)
        {
            if (formFile != null && formFile.Length > 0)
            {
                string filePath = Path.GetTempFileName();

                using var stream = System.IO.File.Create(filePath);
                await formFile.CopyToAsync(stream);
                stream.Seek(0, SeekOrigin.Begin);

                StudentsParsingResult parsingResult = await _csvFileParser.ParseCsvPublicSchoolFile(stream);
                if (parsingResult.InvalidRows.Count != 0)
                {
                    return BadRequest(parsingResult.ErrorMessage);
                }

                StudentsValidationResult validationResult = await _studentBLL.ValidateStudents(parsingResult.Students);
                if (validationResult.InvalidRows.Count != 0)
                {
                    return BadRequest(validationResult.ErrorMessage);
                }

            }

            return Ok();
        }
    }
}
