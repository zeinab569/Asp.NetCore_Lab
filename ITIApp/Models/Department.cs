using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ITIApp.Models
{
    public class Department
    {
        [System.ComponentModel.DataAnnotations.Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "*"), MaxLength(15)]
        public string DepartmentName { get; set; }

        [Required, MaxLength(15)]
        public string DepartmentDescription { get; set; }
        public int Capacity { get; set; }


        public virtual ICollection<Student> Students { get; set; }=new HashSet<Student>();

        public virtual ICollection<Course> Courses { get; set; }=new HashSet<Course>();
    }
}
