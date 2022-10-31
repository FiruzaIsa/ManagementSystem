using ManagementSystem.Dtos;
using ManagementSystem.Models;

namespace ManagementSystem.Extensions
{
    public static class DtoExtensions
    {
        public static CourseDto AsDto(this Course course)
        {
            return new CourseDto
            {
                CourseId = course.CourseID,
                Title = course.Title,
                Credits = course.Credits,
            };
        }
    }
}
