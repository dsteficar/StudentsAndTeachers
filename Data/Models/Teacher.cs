namespace Data.Models
{
    public class Teacher : BaseEntity
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int YearsOfTeaching { get; set; }
        public float Salary { get; set; }
        public bool Associate { get; set; }
        public string? Address { get; set; }
        public virtual ContactInfo? ContactInfo { get; set; }
        public virtual ICollection<Student> Students { get; } = new List<Student>();
        public virtual ICollection<StudentTeacher> StudentTeachers { get; } = new List<StudentTeacher>();
    }
}
