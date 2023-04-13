namespace ITIApp.Models
{
    public class Course
    {
        public int CrsId { get; set; }
        public string Crs_Name { get; set; }
        public string Crs_Description { get; set; }
        public int Lec_Hours { get; set; }
        public int Lab_Hours { get; set; }

        public virtual ICollection<Department> Departments { get; set; } = new HashSet<Department>();

        public virtual ICollection<StudentCourse> Students_has_Courses { get; set; } = new HashSet<StudentCourse>();
    }
}
