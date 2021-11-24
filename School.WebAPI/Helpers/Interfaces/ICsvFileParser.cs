using School.WebAPI.Models;

namespace School.WebAPI.Helpers.Interfaces
{
    public interface ICsvFileParser
    {
        Task<List<Student>> ParseCsvPublicSchoolFile(FileStream fileStream);
    }
}
