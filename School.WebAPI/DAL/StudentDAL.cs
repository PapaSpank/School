using School.WebAPI.Models;
using School.WebAPI.Models.Output;
using System.Reflection;
using System.Text.Json;

namespace School.WebAPI.DAL
{
    public class StudentDAL : IStudentDAL
    {
        private readonly object _timestampLock = new();

        private readonly IConfiguration _configuration;

        public StudentDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // TODO: refactor code
        public async Task InsertPublicSchoolStudents(List<PublicSchoolStudent> students)
        {
            string path = _configuration.GetConnectionString("TimeStampOutput");
            PublicSchoolOutputModel outputModel = new()
            {
                Students = students,
                SchoolName = string.Empty,
                SchoolType = "public"
            };
            
            lock (_timestampLock)
            {
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                path += timestamp + ".json";
            }
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            using FileStream createStream = File.Create(path);
            await JsonSerializer.SerializeAsync(createStream, outputModel, options);
            await createStream.DisposeAsync();

        }
    }
}
