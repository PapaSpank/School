namespace School.WebAPI.Models
{
    public class Student
    {
        //public User() { }
        //public User(string userId, string firstName, string middleName, string lastName, string address, string studentId, string phone, Parent parent, string note = null)
        //{
        //    UserId = userId;
        //    FirstName = firstName;
        //    MiddleName = middleName;
        //    LastName = lastName;
        //    Address = address;
        //    StudentId = studentId;
        //    Phone = phone;
        //    Parent = parent;
        //    Note = note;
        //}
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string StudentId { get; set; }
        public string Phone { get; set; }
        public Parent Parent { get; set; }
        public string Note { get; set; }
    }
}
