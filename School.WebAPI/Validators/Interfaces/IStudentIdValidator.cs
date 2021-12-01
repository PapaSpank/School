using School.WebAPI.Models;
using School.WebAPI.Models.Internal;

namespace School.WebAPI.Validators.Interfaces
{
    public interface IStudentIdValidator
    {
        //Task<TStudentValidationResult> ValidateStudents<TStudentValidationResult, TStudent>(List<TStudent> students);
        Task<PublicStudentsValidationResult> ValidateStudents(List<PublicSchoolStudent> students);
        Task<PrivateStudentsValidationResult> ValidateStudents(List<PrivateSchoolStudent> students);
    }
}
