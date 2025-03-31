using AutoMapper;
using StudentManagement.Data;
using StudentManagement.DTOs;
using StudentManagement.Models;

namespace StudentManagement.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentService> _logger;

        public StudentService(IStudentRepository studentRepository, IMapper mapper, ILogger<StudentService> logger)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<StudentResponseDto> CreateAsync(StudentCreateDto studentCreateDto)
        {
            _logger.LogInformation("Creating new Employee");
            var student = _mapper.Map<Student>(studentCreateDto);
            student.Id = Guid.NewGuid(); // Generating new guid id 

            var createNewStudent = await _studentRepository.CreateAsync(student);
            //_logger.LogInformation("Student data created successfully");

            return _mapper.Map<StudentResponseDto>(createNewStudent);

        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            _logger.LogInformation("Deleting student with Id : {Id}", id);
            return await _studentRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<StudentResponseDto>> GetAllAsync()
        {
            _logger.LogInformation("Getting All student information");
            var student = await _studentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StudentResponseDto>>(student);

        }

        public async Task<StudentResponseDto> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Getting student id with :{id}",id);
           var student = await _studentRepository.GetByIdAsync(id);
            return _mapper.Map<StudentResponseDto>(student);

        }

        public async Task<StudentResponseDto> UpdateAsync(Guid id, StudentUpdateDto studentUpdateDto)
        {
            _logger.LogInformation("Updateing student data with id :{id}", id);
            var ExistingStudent = await _studentRepository.GetByIdAsync(id);

            if(ExistingStudent is null)
            {
                _logger.LogWarning("Student with Id : {id} is not found for update. Please enter valid id", id);
            }
            _mapper.Map(studentUpdateDto, studentUpdateDto);
            var updateStudent = await _studentRepository.UpdateAsync(ExistingStudent);
            return _mapper.Map<StudentResponseDto>(updateStudent);

        }
    }
}
