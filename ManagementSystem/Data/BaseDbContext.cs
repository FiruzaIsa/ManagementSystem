using ManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ManagementSystem.Data
{
    public class BaseDbContext: DbContext
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        {
        }

        public DbSet<Course>? Courses { get; set; }
        public DbSet<Enrollment>? Enrollments { get; set; }
        public DbSet<Student>? Students { get; set; }
        public DbSet<Instructor>? Instructors { get; set; }


    }
}
