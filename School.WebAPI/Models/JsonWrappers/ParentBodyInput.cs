using System.ComponentModel.DataAnnotations;

namespace School.WebAPI.Models.JsonWrappers
{
    public class ParentBodyInput
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
