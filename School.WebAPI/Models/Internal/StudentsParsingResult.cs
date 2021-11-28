namespace School.WebAPI.Models.Internal
{
    public abstract class StudentsParsingResult
    {
        //public List<Student> Students { get; set; }
        public List<int> InvalidRows { get; set; }
        public string ErrorMessage { get; set; }
    }
}
