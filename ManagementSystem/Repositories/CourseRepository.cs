using ManagementSystem.Data;
using ManagementSystem.Models;
using Persistence.Repositories;

namespace ManagementSystem.Repositories
{
    public class CourseRepository : RepositoryBase<Course, BaseDbContext>, ICourseRepository
    {
        public CourseRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
