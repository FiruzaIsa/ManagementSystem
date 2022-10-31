using System.ComponentModel.DataAnnotations;

namespace ManagementSystem.Models
{
    public class Instructor
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Soy Isim")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Isim")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Başlangıç ​​tarih")]
        public DateTime StarDate { get; set; }

        [Display(Name = "Ad Soyad")]
        public string FullName
        {
            get { return LastName + " " + FirstName; }
        }

        public ICollection<Course>? Courses { get; set; }
    }
}
