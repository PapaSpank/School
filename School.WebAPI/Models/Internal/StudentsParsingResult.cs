namespace School.WebAPI.Models.Internal
{
    public abstract class StudentsParsingResult
    {
        public List<int> InvalidRows { get; set; }
        public string ErrorMessage { get; set; }
    }
}
