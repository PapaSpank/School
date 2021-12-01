using School.WebAPI.Models;
using School.WebAPI.Models.Internal;
using School.WebAPI.Validators.Interfaces;
using System.Text;

namespace School.WebAPI.Validators
{
    public class StudentIdValidator : IStudentIdValidator
    {
        public async Task<PublicStudentsValidationResult> ValidateStudents(List<PublicSchoolStudent> students)
        {
            PublicStudentsValidationResult studentsValidationResult = new()
            {
                Students = new(),
                InvalidRows = new()
            };

            var query = students.Select((r, i) => new { Student = r, Index = i }).GroupBy(x => x.Student.StudentId).ToList();

            StringBuilder sb = new("Student ID validation failed for data: ");
            foreach (var group in query)
            {
                // multiple elements with same key exist
                if (group.Count() > 1)
                {
                    sb.Append('{');
                    sb.Append($"student id = \"{group.Key}\", rows = [");
                    foreach (var a in group)
                    {
                        sb.Append($"{a.Index},");
                        studentsValidationResult.InvalidRows.Add(a.Index + 1);
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append("]}, ");
                }
                else
                {
                    studentsValidationResult.Students.Add(group.ToList()[0].Student);
                }
            }
            sb.Remove(sb.Length - 2, 2);


            if (studentsValidationResult.InvalidRows.Any())
            {
                studentsValidationResult.ErrorMessage = sb.ToString();
            }

            studentsValidationResult.ErrorMessage = sb.ToString();

            return studentsValidationResult;
        }

        public async Task<PrivateStudentsValidationResult> ValidateStudents(List<PrivateSchoolStudent> students)
        {
            PrivateStudentsValidationResult studentsValidationResult = new()
            {
                Students = new(),
                InvalidRows = new()
            };

            var query = students.Select((r, i) => new { Student = r, Index = i }).GroupBy(x => x.Student.StudentId).ToList();

            StringBuilder sb = new("Student ID validation failed for data: ");
            foreach (var group in query)
            {
                if (group.Count() > 1)
                {
                    sb.Append('{');
                    sb.Append($"student id = \"{group.Key}\", rows = [");
                    foreach (var a in group)
                    {
                        sb.Append($"{a.Index},");
                        studentsValidationResult.InvalidRows.Add(a.Index + 1);
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append("]}, ");
                }
                else
                {
                    studentsValidationResult.Students.Add(group.ToList()[0].Student);
                }
            }
            sb.Remove(sb.Length - 2, 2);


            if (studentsValidationResult.InvalidRows.Any())
            {
                studentsValidationResult.ErrorMessage = sb.ToString();
            }

            studentsValidationResult.ErrorMessage = sb.ToString();

            return studentsValidationResult;
        }
    }
}
