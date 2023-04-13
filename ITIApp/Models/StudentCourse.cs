using System.ComponentModel.DataAnnotations.Schema;

namespace ITIApp.Models
{
    public class StudentCourse
    {
        [ForeignKey("Student")]
        public int StdId { get; set; }
        [ForeignKey("Course")]
        public int CrsId { get; set; }
        public int? Degree { get; set; }

        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }

    }
}
