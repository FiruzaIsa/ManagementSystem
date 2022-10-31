using Persistence.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Soy isim 50 harfden cok olamaz.")]
        [Display(Name = "Soy Isim")]
        public string LastName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Isim 50 harfden cok olamaz.")]
        [Display(Name = "Isim")]
        public string FirstName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Kayit tarihi")]
        public DateTime EnrollmentDate { get; set; }
        [Display(Name = "Ad Soyad")]
        public string FullName
        {
            get
            {
                return LastName + "  " + FirstName;
            }
        }
        public ICollection<Enrollment> Enrollments { get; set; }

    }
}
