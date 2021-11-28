namespace School.WebAPI.Models
{
    public class PrivateSchoolStudent : Student
    {
        public List<Parent> Parents { get; set; }
    }
}
