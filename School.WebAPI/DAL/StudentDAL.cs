using School.WebAPI.Models;
using School.WebAPI.Models.Output;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace School.WebAPI.DAL
{
    public class StudentDAL : IStudentDAL
    {
        private readonly object _timestampLock = new();
        private readonly JsonSerializerOptions jsonSerializerOptions;

        private readonly IConfiguration _configuration;

        public StudentDAL(IConfiguration configuration)
        {
            _configuration = configuration;
            jsonSerializerOptions = new()
            {
                WriteIndented = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };
        }

        public async Task InsertPublicSchoolStudents(List<PublicSchoolStudent> students, string schoolName)
        {
            PublicSchoolOutputModel outputModel = new()
            {
                Students = students,
                SchoolName = schoolName,
                SchoolType = "public"
            };

            string path = GetOutputFilePath();

            await SaveStudents(outputModel, path);
        }

        public async Task InsertPrivateSchoolStudents(List<PrivateSchoolStudent> students, string schoolName)
        {
            PrivateSchoolOutputModel outputModel = new()
            {
                Students = students,
                SchoolName = schoolName,
                SchoolType = "public"
            };

            string path = GetOutputFilePath();

            await SaveStudents(outputModel, path);
        }

        private async Task SaveStudents<TOutputModel>(TOutputModel outputModel, string path) where TOutputModel : OutputModel
        {
            using FileStream createStream = File.Create(path);
            await JsonSerializer.SerializeAsync(createStream, outputModel, jsonSerializerOptions);
            await createStream.DisposeAsync();
        }

        private string GetOutputFilePath()
        {
            string path = _configuration.GetConnectionString("TimeStampOutput");
            DirectoryInfo d = new(path);
            FileInfo[] files = d.GetFiles("*.json");
            string timestamp;

            lock (_timestampLock)
            {
                timestamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                
            }
            while (Array.IndexOf(files, timestamp) != -1)
            {
                lock (_timestampLock)
                {
                    timestamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                }
            }
            path += timestamp + ".json";
            return path;
        }
    }
}
