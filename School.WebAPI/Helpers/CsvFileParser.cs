using School.WebAPI.Helpers.Interfaces;
using School.WebAPI.Models;

namespace School.WebAPI.Helpers
{
    public class CsvFileParser : ICsvFileParser
    {
        public async Task<List<Student>> ParseCsvPublicSchoolFile(FileStream fileStream)
        {
            List<Student> students = new();

            using StreamReader sr = new(fileStream);
            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] splitFields = line.Split(',');
                // test for number of fields - 10 (without note) or 11 (with note fields)
                if (!(splitFields.Length == 10 || splitFields.Length == 11))
                {
                    // data not in right format, continue
                    //break;
                    continue;
                }
                string userId          = splitFields[0];
                string firstName       = splitFields[1];
                string middleName      = splitFields[2];
                string lastName        = splitFields[3];
                string address         = splitFields[4];
                string studentId       = splitFields[5];
                string phone           = splitFields[6];
                string parentFirstName = splitFields[7];
                string parentLastName  = splitFields[8];
                string parentPhone     = splitFields[9];
                string note = null;
                if (splitFields.Length == 11)
                {
                    note = splitFields[10];
                }

                Parent parent = new()
                {
                    FirstName = parentFirstName,
                    LastName = parentLastName,
                    Phone = parentPhone
                };

                Student student = new()
                {
                    UserId = userId,
                    FirstName = firstName,
                    MiddleName = middleName,
                    LastName = lastName,
                    Address = address,
                    StudentId = studentId,
                    Phone = phone,
                    Parent = parent,
                    Note = note
                };

                students.Add(student);
            }

            return students;
        }
    }
}
