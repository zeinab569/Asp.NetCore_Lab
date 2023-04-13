using Microsoft.EntityFrameworkCore;

namespace ITIApp.Models
{
    public class TheITIContext: DbContext
    {
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Student> Students { get; set; }    
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;database=ITIManagement;trusted_connection=true;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>().HasKey(a => new { a.StdId, a.CrsId });
            modelBuilder.Entity<Course>().HasKey(a => a.CrsId);
           
            base.OnModelCreating(modelBuilder);
        }
    }
}
