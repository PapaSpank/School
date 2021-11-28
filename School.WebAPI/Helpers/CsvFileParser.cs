using School.WebAPI.Helpers.Interfaces;
using School.WebAPI.Models;
using School.WebAPI.Models.Internal;
using System.Text;

namespace School.WebAPI.Helpers
{
    public class CsvFileParser : ICsvFileParser
    {
        public async Task<PrivateStudentsParsingResult> ParseCsvPrivateSchoolFile(FileStream fileStream)
        {
            PrivateStudentsParsingResult parsingResult = new()
            {
                Students = new(),
                InvalidRows = new(),
                ErrorMessage = string.Empty
            };
            bool parsingFailed = false;

            using StreamReader sr = new(fileStream);
            string? line;
            int lineCounter = 0;
            while ((line = sr.ReadLine()) != null)
            {
                lineCounter++;

                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] splitFields = line.Split(',');
                // test for number of fields - 13 (without note) or 14 (with note fields)
                if (!(splitFields.Length == 13 || splitFields.Length == 14))
                {
                    // data not in right format, continue
                    //break;
                    parsingFailed = true;
                    parsingResult.InvalidRows.Add(lineCounter);
                    continue;
                }
                string userId = splitFields[0];
                string firstName = splitFields[1];
                string middleName = splitFields[2];
                string lastName = splitFields[3];
                string address = splitFields[4];
                string studentId = splitFields[5];
                string phone = splitFields[6];
                string motherFirstName = splitFields[7];
                string motherLastName = splitFields[8];
                string motherPhone = splitFields[9];
                string fatherFirstName = splitFields[10];
                string fatherLastName = splitFields[11];
                string fatherPhone = splitFields[12];
                string note = null;
                if (splitFields.Length == 14)
                {
                    note = splitFields[13];
                }

                Parent mother = new()
                {
                    FirstName = motherFirstName,
                    LastName = motherLastName,
                    Phone = motherPhone
                };
                Parent father = new()
                {
                    FirstName = fatherFirstName,
                    LastName = fatherLastName,
                    Phone = fatherPhone
                };

                PrivateSchoolStudent student = new()
                {
                    UserId = userId,
                    FirstName = firstName,
                    MiddleName = middleName,
                    LastName = lastName,
                    Address = address,
                    StudentId = studentId,
                    Phone = phone,
                    Parents = new List<Parent> { mother, father },
                    Note = note
                };

                parsingResult.Students.Add(student);
            }

            if (parsingFailed)
            {
                StringBuilder sb = new("Parsing failed for data with row numbers: [ ");
                foreach (int row in parsingResult.InvalidRows)
                {
                    sb.Append(row + ", ");
                }
                sb.Remove(sb.Length - 2, 2);
                sb.Append(" ]");

                parsingResult.ErrorMessage = sb.ToString();
            }

            return parsingResult;
        }

        public async Task<PublicStudentsParsingResult> ParseCsvPublicSchoolFile(FileStream fileStream)
        {
            PublicStudentsParsingResult parsingResult = new()
            {
                Students = new(),
                InvalidRows = new(),
                ErrorMessage = string.Empty
            };
            bool parsingFailed = false;

            using StreamReader sr = new(fileStream);
            string? line;
            int lineCounter = 0;
            while ((line = sr.ReadLine()) != null)
            {
                lineCounter++;

                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] splitFields = line.Split(',');
                // test for number of fields - 10 (without note) or 11 (with note fields)
                if (!(splitFields.Length == 10 || splitFields.Length == 11))
                {
                    // data not in right format, continue
                    //break;
                    parsingFailed = true;
                    parsingResult.InvalidRows.Add(lineCounter);
                    continue;
                }
                string userId = splitFields[0];
                string firstName = splitFields[1];
                string middleName = splitFields[2];
                string lastName = splitFields[3];
                string address = splitFields[4];
                string studentId = splitFields[5];
                string phone = splitFields[6];
                string parentFirstName = splitFields[7];
                string parentLastName = splitFields[8];
                string parentPhone = splitFields[9];
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

                PublicSchoolStudent student = new()
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

                parsingResult.Students.Add(student);
            }

            if (parsingFailed)
            {
                StringBuilder sb = new("Parsing failed for data with row numbers: [ ");
                foreach (int row in parsingResult.InvalidRows)
                {
                    sb.Append(row + ", ");
                }
                sb.Remove(sb.Length - 2, 2);
                sb.Append(" ]");

                parsingResult.ErrorMessage = sb.ToString();
            }

            return parsingResult;
        }
    }
}
