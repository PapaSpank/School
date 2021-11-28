using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.WebAPI.BLL;
using School.WebAPI.DAL;
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
        private readonly IStudentDAL _studentDAL;

        public StudentsController(ICsvFileParser csvFileParser,
            IStudentBLL studentBLL,
            IStudentDAL studentDAL)
        {
            _csvFileParser = csvFileParser;
            _studentBLL = studentBLL;
            _studentDAL = studentDAL;
        }

        [HttpPost]
        [Route("createFromBodyPublic")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateStudentsFromBody([FromBody] StudentListWrapper studentsWrapper)
        {
            List<PublicSchoolStudent> students = new();

            foreach (var studentInput in studentsWrapper.Users)
            {
                PublicSchoolStudent student = new()
                {
                    UserId = studentInput.UserId,
                    FirstName = studentInput.FirstName,
                    MiddleName = studentInput.MiddleName,
                    LastName = studentInput.LastName,
                    StudentId = studentInput.StudentId,
                    Phone = studentInput.Phone,
                    Parent = new Parent()
                    {
                        FirstName = studentInput.Parent.FirstName,
                        MiddleName = studentInput.Parent.MiddleName,
                        LastName = studentInput.Parent.LastName,
                        Phone = studentInput.Parent.Phone
                    },
                    Note = studentInput.Note,
                };
                students.Add(student);
            }

            PublicStudentsValidationResult validationResult = await _studentBLL.ValidatePublicSchoolStudentsFromBody(students);
            if (validationResult.InvalidRows.Count != 0)
            {
                return BadRequest(validationResult.ErrorMessage);
            }

            try
            {
                await _studentDAL.InsertPublicSchoolStudents(validationResult.Students);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"{ex.Message}" });
            }

            return Ok();
        }

        [HttpPost]
        [Route("createFromFilePublic")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateStudentsFromFile([FromForm] IFormFile formFile)
        {
            if (formFile == null || formFile.Length == 0)
            {
                return BadRequest("File cannot be null or empty");
            }

            string filePath = Path.GetTempFileName();

            using var stream = System.IO.File.Create(filePath);
            await formFile.CopyToAsync(stream);
            stream.Seek(0, SeekOrigin.Begin);

            PublicStudentsParsingResult parsingResult = await _csvFileParser.ParseCsvPublicSchoolFile(stream);
            if (parsingResult.InvalidRows.Count != 0)
            {
                return BadRequest(parsingResult.ErrorMessage);
            }
            List<PublicSchoolStudent> ss = parsingResult.Students;

            PublicStudentsValidationResult validationResult = await _studentBLL.ValidatePublicSchoolStudentsFromFile(parsingResult.Students);
            if (validationResult.InvalidRows.Count != 0)
            {
                return BadRequest(validationResult.ErrorMessage);
            }
            // TODO: remoteIpAddress
            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
            string address = remoteIpAddress.ToString();

            try
            {
                await _studentDAL.InsertPublicSchoolStudents(validationResult.Students);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"{ex.Message}" });
            }

            return Ok();
        }

        //[HttpPost]
        //[Route("createFromFilePrivate")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> CreateStudentsFromFilePrivate([FromForm] IFormFile formFile)
        //{
        //    if (formFile == null || formFile.Length == 0)
        //    {
        //        return BadRequest("File cannot be null or empty");
        //    }

        //    string filePath = Path.GetTempFileName();

        //    using var stream = System.IO.File.Create(filePath);
        //    await formFile.CopyToAsync(stream);
        //    stream.Seek(0, SeekOrigin.Begin);

        //    StudentsParsingResult parsingResult = await _csvFileParser.ParseCsvPrivateSchoolFile(stream);
        //    if (parsingResult.InvalidRows.Count != 0)
        //    {
        //        return BadRequest(parsingResult.ErrorMessage);
        //    }

        //    StudentsValidationResult validationResult = await _studentBLL.ValidatePrivateSchoolStudentsFromFile(parsingResult.Students);
        //    if (validationResult.InvalidRows.Count != 0)
        //    {
        //        return BadRequest(validationResult.ErrorMessage);
        //    }
        //    // TODO: remoteIpAddress
        //    var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
        //    string address = remoteIpAddress.ToString();

        //    try
        //    {
        //        await _studentDAL.InsertPrivateSchoolStudents(validationResult.Students);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"{ex.Message}" });
        //    }

        //    return Ok();
        //}
    }
}
