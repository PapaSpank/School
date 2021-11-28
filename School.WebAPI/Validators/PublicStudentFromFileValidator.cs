using School.WebAPI.Helpers.Interfaces;
using School.WebAPI.Models;
using School.WebAPI.Models.Internal;
using School.WebAPI.Validators.Interfaces;
using System.Text;

namespace School.WebAPI.Validators
{
    public class PublicStudentFromFileValidator : IPublicStudentFromFileValidator
    {
        public async Task<PublicStudentsValidationResult> ValidateStudents(List<PublicSchoolStudent> students)
        {
            bool validationFailed = false;
            PublicStudentsValidationResult studentsValidationResult = new()
            {
                Students = new(),
                InvalidRows = new(),
                ErrorMessage = string.Empty
            };

            for (int i = 0; i < students.Count; i++)
            {
                PublicSchoolStudent student = students[i];
                if (ValidateStudent(student))
                {
                    studentsValidationResult.Students.Add(student);
                }
                else
                {
                    validationFailed = true;
                    studentsValidationResult.InvalidRows.Add(i + 1);
                }
            }
            if (validationFailed)
            {
                StringBuilder sb = new("Validation failed for data with row numbers: [ ");
                foreach (int row in studentsValidationResult.InvalidRows)
                {
                    sb.Append(row + ", ");
                }
                sb.Remove(sb.Length - 2, 2);
                sb.Append(" ]");

                studentsValidationResult.ErrorMessage = sb.ToString();
            }

            return studentsValidationResult;
        }

        private static bool ValidateStudent(PublicSchoolStudent student)
        {
            if (string.IsNullOrEmpty(student.UserId))
                return false;
            if (string.IsNullOrEmpty(student.FirstName))
                return false;
            if (string.IsNullOrEmpty(student.MiddleName))
                return false;
            if (string.IsNullOrEmpty(student.LastName))
                return false;
            if (string.IsNullOrEmpty(student.Address))
                return false;
            if (string.IsNullOrEmpty(student.StudentId))
                return false;
            if (string.IsNullOrEmpty(student.Phone))
                return false;

            if (string.IsNullOrEmpty(student.Parent.FirstName))
                return false;
            if (string.IsNullOrEmpty(student.Parent.LastName))
                return false;
            if (string.IsNullOrEmpty(student.Parent.Phone))
                return false;

            return true;
        }
    }
}
