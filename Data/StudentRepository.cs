using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;

namespace StudentManagement.Data
{
    public class StudentRepository : IStudentRepository
    {

        private readonly StudentDbContext _context;

        public StudentRepository(StudentDbContext context)
        {
            _context = context;
        }

        public async Task<Student> CreateAsync(Student student)
        {
            _context.students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
           var student = await _context.students.FindAsync(id);
            if(student is null)
            {
                return false;
            }
            _context.Remove(student);
            _context.SaveChanges();

            return true;

        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.students.AsNoTracking().ToListAsync();
        }

        public async Task<Student> GetByIdAsync(Guid id)
        {
            var student = await _context.students.FindAsync(id);
            if(student == null)
            {
                throw new Exception("Student with Id {id} is not found");
            }

            return student;
           
        }

        public async Task<Student> UpdateAsync(Student student)
        {
            _context.students.Update(student);
            await _context.SaveChangesAsync();
            return student;
        }
    }
}
