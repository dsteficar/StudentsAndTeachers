namespace Data.Models
{
    public class SchoolClass : BaseEntity
    {
        public string? Name { get; set; }
        public int StudentCapacity { get; set; }
        public bool Online { get; set; }
        public virtual ICollection<Student> Students { get; } = new List<Student>();
    }
}
