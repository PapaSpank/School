using School.WebAPI.Models;
using School.WebAPI.Models.Internal;

namespace School.WebAPI.Helpers.Interfaces
{
    public interface ICsvFileParser
    {
        Task<PublicStudentsParsingResult> ParseCsvPublicSchoolFile(FileStream fileStream);
    }
}
