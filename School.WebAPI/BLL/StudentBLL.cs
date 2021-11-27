using School.WebAPI.Models;
using School.WebAPI.Models.Internal;
using School.WebAPI.Validators.Interfaces;

namespace School.WebAPI.BLL
{
    public class StudentBLL : IStudentBLL
    {
        private readonly IStudentValidator _studentValidator;

        public StudentBLL(IStudentValidator studentValidator)
        {
            _studentValidator = studentValidator;
        }

        public async Task<StudentsValidationResult> ValidateStudents(List<Student> students)
        {
            StudentsValidationResult studentValidationResult = await _studentValidator.ValidateStudents(students);
            // short-circuit processing if there are row which failed validation
            if (studentValidationResult.InvalidRows.Count != 0)
                return studentValidationResult;


            // multiple validations occured, return end result
            return studentValidationResult;
        }
    }
}
