namespace StudentManagement.DTOs
{
    public class StudentResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
