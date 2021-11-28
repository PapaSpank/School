namespace School.WebAPI.Models.Internal
{
    public abstract class StudentsValidationResult
    {
        //public List<Student> Students { get; set; }
        public List<int> InvalidRows { get; set; }
        public string ErrorMessage { get; set; }
    }
}
