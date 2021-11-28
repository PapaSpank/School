using School.WebAPI.Models;
using School.WebAPI.Models.Internal;

namespace School.WebAPI.BLL
{
    public interface IStudentBLL
    {
        Task<PublicStudentsValidationResult> ValidatePublicSchoolStudentsFromFile(List<PublicSchoolStudent> students);
        Task<PrivateStudentsValidationResult> ValidatePrivateSchoolStudentsFromFile(List<PrivateSchoolStudent> students);
        Task<PublicStudentsValidationResult> ValidatePublicSchoolStudentsFromBody(List<PublicSchoolStudent> students);
    }
}
