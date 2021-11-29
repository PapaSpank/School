using School.WebAPI.Models;
using School.WebAPI.Models.Internal;

namespace School.WebAPI.Validators.Interfaces
{
    public interface IPrivateStudentFromFileValidator
    {
        Task<PrivateStudentsValidationResult> ValidateStudents(List<PrivateSchoolStudent> students);
    }
}
