using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ManagementSystem.Models
{
    public class Enrollment
    {
        
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        [DisplayFormat(NullDisplayText = "Not yok")]
        [DisplayName("Not")]
        public Grade? Grade { get; set; }

        public Course? Course { get; set; }
        public Student? Student { get; set; }
    }
}
