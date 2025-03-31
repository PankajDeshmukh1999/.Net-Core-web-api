using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.DTOs;
using StudentManagement.Exceptions;
using StudentManagement.Services;

namespace StudentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(IStudentService studentService, ILogger<StudentsController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentResponseDto>>> GetAllStudent()
        {
            var students = await _studentService.GetAllAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentResponseDto>> GetStudentById (Guid id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if(student is null)
            {
                _logger.LogWarning("Student with ID: {Id} not found", id);
                throw new NotFoundException($"Student with ID {id} not found");
            }

            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<StudentResponseDto>> CreateStudent (StudentCreateDto studentCreateDto)
        {
            var createStudent = await _studentService.CreateAsync(studentCreateDto);
            return Ok(createStudent);
        }

        [HttpPut]
        public async Task<ActionResult<StudentResponseDto>> UpdateStudent(Guid id, StudentUpdateDto studentUpdateDto)
        {
            var updateStudent = await _studentService.UpdateAsync(id, studentUpdateDto);
            if (updateStudent == null)
            {
                _logger.LogWarning("Student with ID: {Id} not found for update", id);
                throw new NotFoundException($"Student with ID {id} not found");
            }
            return Ok(updateStudent);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<StudentResponseDto>> DeleteStudent (Guid id)
        {
            var result = await _studentService.DeleteAsync(id);
            if(!result)
            {
                _logger.LogWarning("Student with ID: {Id} not found for deletion", id);
                throw new NotFoundException($"Student with ID {id} not found");
            }

            return NoContent();
        }

    }
}
