namespace Data.Models
{
    public class TeacherDTO : BaseEntity
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int YearsOfTeaching { get; set; }
        public float Salary { get; set; }
        public bool Associate { get; set; }
        public string? Address { get; set; }
    }
}
