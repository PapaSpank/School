namespace School.WebAPI.Models
{
    public abstract class Student
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string StudentId { get; set; }
        public string Phone { get; set; }
        public string Note { get; set; }
    }
}
