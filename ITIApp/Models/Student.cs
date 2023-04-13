using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITIApp.Models
{
    public class Student
    {
        //[System.ComponentModel.DataAnnotations.Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int Id { get; set; }

        public string StudentName { get; set; }
        public string StudentEmail { get; set; }
        public string StudentPhone { get; set; }
        [Range(20,30)]
        public int Age { get; set; }
        public string Password { get; set; }
        [Compare("Password")]
        [DataType(DataType.Password)]
        [NotMapped]
        public string CPassword { get; set; }
        public int? Degree { set; get; }

        [ForeignKey("Department")]
        public int DeptNo { get; set; }

        public virtual Department Department { get; set; }

        public virtual ICollection<StudentCourse> StudentCourses { get; set; }=new HashSet<StudentCourse>();
    }
}
