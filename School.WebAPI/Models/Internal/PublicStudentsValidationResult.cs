namespace School.WebAPI.Models.Internal
{
    public class PublicStudentsValidationResult : StudentsValidationResult
    {
        public List<PublicSchoolStudent> Students { get; set; }
    }
}
