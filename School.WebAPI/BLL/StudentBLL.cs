using School.WebAPI.Models;
using School.WebAPI.Models.Internal;
using School.WebAPI.Validators.Interfaces;

namespace School.WebAPI.BLL
{
    public class StudentBLL : IStudentBLL
    {
        private readonly IPublicStudentFromFileValidator _publicStudentFromFileValidator;
        private readonly IPublicStudentFromBodyValidator _publicStudentFromBodyValidator;
        private readonly IPrivateStudentFromFileValidator _privateStudentFromFileValidator;
        private readonly IStudentIdValidator _studentIdValidator;

        public StudentBLL(IPublicStudentFromFileValidator studentValidator,
            IPublicStudentFromBodyValidator publicStudentFromBodyValidator,
            IPrivateStudentFromFileValidator privateStudentFromFileValidator,
            IStudentIdValidator studentIdValidator)
        {
            _publicStudentFromFileValidator = studentValidator;
            _publicStudentFromBodyValidator = publicStudentFromBodyValidator;
            _privateStudentFromFileValidator = privateStudentFromFileValidator;
            _studentIdValidator = studentIdValidator;
        }

        public async Task<PublicStudentsValidationResult> ValidatePublicSchoolStudentsFromFile(List<PublicSchoolStudent> students)
        {
            PublicStudentsValidationResult studentValidationResult = await _publicStudentFromFileValidator.ValidateStudents(students);
            // short-circuit processing if there are row which failed validation
            if (studentValidationResult.InvalidRows.Count != 0)
                return studentValidationResult;

            studentValidationResult = await _studentIdValidator.ValidateStudents(studentValidationResult.Students);
            if (studentValidationResult.InvalidRows.Count != 0)
                return studentValidationResult;

            // multiple validations occured, return end result
            return studentValidationResult;
        }

        public async Task<PublicStudentsValidationResult> ValidatePublicSchoolStudentsFromBody(List<PublicSchoolStudent> students)
        {
            PublicStudentsValidationResult studentValidationResult = await _publicStudentFromBodyValidator.ValidateStudents(students);
            // short-circuit processing if there are row which failed validation
            if (studentValidationResult.InvalidRows.Count != 0)
                return studentValidationResult;

            studentValidationResult = await _studentIdValidator.ValidateStudents(studentValidationResult.Students);
            if (studentValidationResult.InvalidRows.Count != 0)
                return studentValidationResult;

            // multiple validations occured, return end result
            return studentValidationResult;
        }

        public async Task<PrivateStudentsValidationResult> ValidatePrivateSchoolStudentsFromFile(List<PrivateSchoolStudent> students)
        {
            PrivateStudentsValidationResult studentValidationResult = await _privateStudentFromFileValidator.ValidateStudents(students);
            // short-circuit processing if there are row which failed validation
            if (studentValidationResult.InvalidRows.Count != 0)
                return studentValidationResult;

            studentValidationResult = await _studentIdValidator.ValidateStudents(studentValidationResult.Students);
            if (studentValidationResult.InvalidRows.Count != 0)
                return studentValidationResult;

            // multiple validations occured, return end result
            return studentValidationResult;
        }
    }
}
