using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
#pragma warning disable CS1591
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> UserList { get; set; } = null!;
        public DbSet<Student> StudentList { get; set; } = null!;
        public DbSet<Teacher> TeacherList { get; set; } = null!;
        public DbSet<ContactInfo> ContactInfoList { get; set; } = null!;
        public DbSet<SchoolClass> SchoolClassList { get; set; } = null!;
        public DbSet<StudentTeacher> StudentTeacherList { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Teacher>().ToTable("Teacher");
            modelBuilder.Entity<ContactInfo>().ToTable("ContactInfo");
            modelBuilder.Entity<SchoolClass>().ToTable("SchoolClass");
            modelBuilder.Entity<StudentTeacher>().ToTable("StudentTeacher");

            modelBuilder.Entity<Student>()
                .ToTable("Student");
            modelBuilder.Entity<Teacher>()
                .ToTable("Teacher");

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Teachers)
                .WithMany(e => e.Students)
                .UsingEntity<StudentTeacher>();
        }
    }
#pragma warning restore CS1591
}
