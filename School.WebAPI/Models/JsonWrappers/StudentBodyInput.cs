using System.ComponentModel.DataAnnotations;

namespace School.WebAPI.Models.JsonWrappers
{
    public class StudentBodyInput
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string StudentId { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public ParentBodyInput Parent { get; set; }
        public string? Note { get; set; }
    }
}
