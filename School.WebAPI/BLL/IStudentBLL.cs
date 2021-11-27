﻿using School.WebAPI.Models;
using School.WebAPI.Models.Internal;

namespace School.WebAPI.BLL
{
    public interface IStudentBLL
    {
        Task<StudentsValidationResult> ValidateStudents(List<Student> students);
    }
}
