using School.WebAPI.Models;

namespace School.WebAPI.Helpers.Interfaces
{
    public interface IStudentValidation
    {
        Task ValidateStudents(List<Student> students);
    }
}
