using System.ComponentModel.DataAnnotations;

namespace StudentManagement.DTOs
{
    public class StudentCreateDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}
