using School.WebAPI.Models;
using School.WebAPI.Models.Internal;
using System.Text;

namespace School.WebAPI.Validators.Interfaces
{
    public class PrivateStudentFromFileValidator : IPrivateStudentFromFileValidator
    {
        public async Task<PrivateStudentsValidationResult> ValidateStudents(List<PrivateSchoolStudent> students)
        {
            bool validationFailed = false;
            PrivateStudentsValidationResult studentsValidationResult = new()
            {
                Students = new(),
                InvalidRows = new(),
                ErrorMessage = string.Empty
            };

            for (int i = 0; i < students.Count; i++)
            {
                PrivateSchoolStudent student = students[i];
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

        private static bool ValidateStudent(PrivateSchoolStudent student)
        {
            if (string.IsNullOrEmpty(student.UserId))
                return false;
            if (string.IsNullOrEmpty(student.FirstName))
                return false;
            if (string.IsNullOrEmpty(student.MiddleName))
                return false;
            if (string.IsNullOrEmpty(student.LastName))
                return false;
            if (string.IsNullOrEmpty(student.StudentId))
                return false;
            if (string.IsNullOrEmpty(student.Phone))
                return false;

            if (student.Parents.Count == 0 || student.Parents.Count > 2)
                return false;

            bool firstParentValidated = false;
            if (!string.IsNullOrEmpty(student.Parents[0].FirstName) &&
                !string.IsNullOrEmpty(student.Parents[0].LastName) &&
                !string.IsNullOrEmpty(student.Parents[0].Phone))
            {
                firstParentValidated = true;
            }
            bool secondParentValidated = false;
            if (student.Parents.Count == 2)
            {
                if (student.Parents[1] != null &&
                !string.IsNullOrEmpty(student.Parents[1].FirstName) &&
                !string.IsNullOrEmpty(student.Parents[1].LastName) &&
                !string.IsNullOrEmpty(student.Parents[1].Phone))
                {
                    secondParentValidated = true;
                }
            }
            else
            {
                secondParentValidated = true;
            }

            if (firstParentValidated && secondParentValidated)
            {
                return true;
            }

            return false;
        }
    }
}
