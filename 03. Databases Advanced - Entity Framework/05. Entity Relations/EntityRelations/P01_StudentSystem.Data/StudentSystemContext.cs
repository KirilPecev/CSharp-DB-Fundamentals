namespace P01_StudentSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {

        }

        public StudentSystemContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Homework> HomeworkSubmissions { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(p => p.PhoneNumber)
                    .HasColumnType("char(10)")
                    .IsUnicode(false);

                entity.HasMany(e => e.HomeworkSubmissions)
                    .WithOne(e => e.Student);
            });

            modelBuilder.Entity<StudentCourse>(entity => { entity.HasKey(k => new { k.StudentId, k.CourseId }); });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasMany(e => e.HomeworkSubmissions)
                    .WithOne(e => e.Course);

                entity.HasMany(e => e.Resources)
                    .WithOne(e => e.Course);
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasOne(e => e.Student)
                    .WithMany(e => e.CourseEnrollments);

                entity.HasOne(e => e.Course)
                    .WithMany(e => e.StudentsEnrolled);
            });
        }
    }
}
