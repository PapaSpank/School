using School.WebAPI.Models;
using School.WebAPI.Models.Internal;

namespace School.WebAPI.Validators.Interfaces
{
    public interface IPublicStudentFromFileValidator
    {
        Task<PublicStudentsValidationResult> ValidateStudents(List<PublicSchoolStudent> students);
    }
}
