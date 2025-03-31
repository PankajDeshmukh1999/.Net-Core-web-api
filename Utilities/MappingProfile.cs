using AutoMapper;
using StudentManagement.DTOs;
using StudentManagement.Models;

namespace StudentManagement.Utilities
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
    {
        // Add this mapping configuration
        CreateMap<StudentCreateDto, Student>();
        CreateMap<StudentUpdateDto, Student>();
        CreateMap<Student, StudentResponseDto>();
    }
    
    }
}
