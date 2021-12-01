using School.WebAPI.Models;

namespace School.WebAPI.DAL
{
    public interface IStudentDAL
    {
        Task InsertPublicSchoolStudents(List<PublicSchoolStudent> students, string schoolName);
        Task InsertPrivateSchoolStudents(List<PrivateSchoolStudent> students, string schoolName);
    }
}
