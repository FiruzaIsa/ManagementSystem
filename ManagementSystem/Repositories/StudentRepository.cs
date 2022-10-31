using ManagementSystem.Data;
using ManagementSystem.Models;
using Persistence.Repositories;

namespace ManagementSystem.Repositories
{
    public class StudentRepository : RepositoryBase<Student, BaseDbContext>, IStudentRepository
    {
        public StudentRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
