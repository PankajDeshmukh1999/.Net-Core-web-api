using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;

namespace StudentManagement.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions options) : base(options)
        {
        }

        protected StudentDbContext()
        {
        }

         public DbSet<Student> students { get; set; }

        // database validation  on each feild 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Email).IsUnique();
            });
        }
    }
}
