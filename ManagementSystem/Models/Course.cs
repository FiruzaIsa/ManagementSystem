using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Numara")]
        public int CourseID { get; set; }
        [Required(ErrorMessage ="Ders ismi gerekli")]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Ders")]
        public string Title { get; set; }

        [Range(0, 5)]
        [Display(Name = "Kredi")]
        public int Credits { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
