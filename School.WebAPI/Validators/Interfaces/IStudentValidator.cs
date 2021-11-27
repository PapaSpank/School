using School.WebAPI.Models;
using School.WebAPI.Models.Internal;

namespace School.WebAPI.Validators.Interfaces
{
    public interface IStudentValidator
    {
        Task<StudentsValidationResult> ValidateStudents(List<Student> students);
    }
}
