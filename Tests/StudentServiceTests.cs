using AutoMapper;
using Moq;
using StudentManagement.Data;
using StudentManagement.DTOs;
using StudentManagement.Models;
using StudentManagement.Services;
using StudentManagement.Utilities;
using Xunit;

namespace StudentManagement.Tests
{
    public class StudentServiceTests
    {
        private readonly Mock<IStudentRepository> _mockRepo;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<StudentService>> _mockLogger;
        private readonly StudentManagement.Services.StudentService _service;

        public StudentServiceTests()
        {
            _mockRepo = new Mock<IStudentRepository>();
            _mockLogger = new Mock<ILogger<StudentService>>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();

            _service = new StudentManagement.Services.StudentService(
                _mockRepo.Object, _mapper, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllStudents()
        {
            // Arrange
            var students = new List<Student>
            {
                new Student { Id = Guid.NewGuid(), Name = "Test Student 1" },
                new Student { Id = Guid.NewGuid(), Name = "Test Student 2" }
            };

            _mockRepo.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(students);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
            _mockRepo.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_StudentExists_ReturnsStudent()
        {
            // Arrange
            var studentId = Guid.NewGuid();
            var student = new Student { Id = studentId, Name = "Test Student" };

            _mockRepo.Setup(repo => repo.GetByIdAsync(studentId))
                .ReturnsAsync(student);

            // Act
            var result = await _service.GetByIdAsync(studentId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(studentId, result.Id);
            _mockRepo.Verify(repo => repo.GetByIdAsync(studentId), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_StudentNotExists_ReturnsNull()
        {
            // Arrange
            var studentId = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.GetByIdAsync(studentId))
                .ReturnsAsync((Student)null);

            // Act
            var result = await _service.GetByIdAsync(studentId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ValidStudent_ReturnsCreatedStudent()
        {
            // Arrange
            var studentCreateDto = new StudentCreateDto
            {
                Name = "New Student",
                Email = "new@example.com"
                
            };

            _mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<Student>()))
                .ReturnsAsync((Student s) => s);

            // Act
            var result = await _service.CreateAsync(studentCreateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(studentCreateDto.Name, result.Name);
            _mockRepo.Verify(repo => repo.CreateAsync(It.IsAny<Student>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_StudentExists_UpdatesStudent()
        {
            // Arrange
            var studentId = Guid.NewGuid();
            var existingStudent = new Student
            {
                Id = studentId,
                Name = "Original Name",
                Email = "test@example.com"
            };

            var updateDto = new StudentUpdateDto { Name = "Updated Name" };

            _mockRepo.Setup(repo => repo.GetByIdAsync(studentId))
                .ReturnsAsync(existingStudent);

            _mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Student>()))
                .ReturnsAsync((Student s) => s);

            // Act
            var result = await _service.UpdateAsync(studentId, updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updateDto.Name, result.Name);
            _mockRepo.Verify(repo => repo.GetByIdAsync(studentId), Times.Once);
            _mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Student>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_StudentNotExists_ReturnsNull()
        {
            // Arrange
            var studentId = Guid.NewGuid();
            var updateDto = new StudentUpdateDto { Name = "Updated Name" };

            _mockRepo.Setup(repo => repo.GetByIdAsync(studentId))
                .ReturnsAsync((Student)null);

            // Act
            var result = await _service.UpdateAsync(studentId, updateDto);

            // Assert
            Assert.Null(result);
            _mockRepo.Verify(repo => repo.GetByIdAsync(studentId), Times.Once);
            _mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Student>()), Times.Never);
        }

        [Fact]
        public async Task DeleteAsync_StudentExists_ReturnsTrue()
        {
            // Arrange
            var studentId = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.DeleteAsync(studentId))
                .ReturnsAsync(true);

            // Act
            var result = await _service.DeleteAsync(studentId);

            // Assert
            Assert.True(result);
            _mockRepo.Verify(repo => repo.DeleteAsync(studentId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_StudentNotExists_ReturnsFalse()
        {
            // Arrange
            var studentId = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.DeleteAsync(studentId))
                .ReturnsAsync(false);

            // Act
            var result = await _service.DeleteAsync(studentId);

            // Assert
            Assert.False(result);
            _mockRepo.Verify(repo => repo.DeleteAsync(studentId), Times.Once);
        }
    }
}

