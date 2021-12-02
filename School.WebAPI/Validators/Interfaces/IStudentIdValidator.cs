using School.WebAPI.Models;
using School.WebAPI.Models.Internal;

namespace School.WebAPI.Validators.Interfaces
{
    public interface IStudentIdValidator
    {
        Task<PublicStudentsValidationResult> ValidateStudents(List<PublicSchoolStudent> students);
        Task<PrivateStudentsValidationResult> ValidateStudents(List<PrivateSchoolStudent> students);
    }
}
