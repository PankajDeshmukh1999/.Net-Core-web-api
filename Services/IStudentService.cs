using StudentManagement.DTOs;

namespace StudentManagement.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentResponseDto>> GetAllAsync();
        Task<StudentResponseDto> GetByIdAsync(Guid id);
        Task<StudentResponseDto> CreateAsync(StudentCreateDto studentCreateDto);
        Task<StudentResponseDto> UpdateAsync(Guid id, StudentUpdateDto studentUpdateDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
