using School.WebAPI.Models;
using School.WebAPI.Models.Internal;

namespace School.WebAPI.Validators.Interfaces
{
    public interface IPublicStudentFromBodyValidator
    {
        Task<PublicStudentsValidationResult> ValidateStudents(List<PublicSchoolStudent> students);
    }
}
