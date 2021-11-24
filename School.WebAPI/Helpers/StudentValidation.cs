using School.WebAPI.Helpers.Interfaces;
using School.WebAPI.Models;

namespace School.WebAPI.Helpers
{
    public class StudentValidation : IStudentValidation
    {
        public async Task ValidateStudents(List<Student> students)
        {
            List<Student> validatedStudents = new();
            foreach (Student student in students)
            {
                if (ValidateStudent(student))
                    validatedStudents.Add(student);
            }
        }

        private bool ValidateStudent(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
