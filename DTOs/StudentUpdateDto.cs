using System.ComponentModel.DataAnnotations;

namespace StudentManagement.DTOs
{
    public class StudentUpdateDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}
