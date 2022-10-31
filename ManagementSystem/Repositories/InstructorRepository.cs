using ManagementSystem.Data;
using ManagementSystem.Models;
using Persistence.Repositories;

namespace ManagementSystem.Repositories
{
    public class InstructorRepository : RepositoryBase<Instructor, BaseDbContext>, IInstructorRepository
    {
        public InstructorRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
