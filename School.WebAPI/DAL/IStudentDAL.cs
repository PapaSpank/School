using School.WebAPI.Models;

namespace School.WebAPI.DAL
{
    public interface IStudentDAL
    {
        Task InsertPublicSchoolStudents(List<PublicSchoolStudent> students);
        Task InsertPrivateSchoolStudents(List<PrivateSchoolStudent> students);
    }
}
