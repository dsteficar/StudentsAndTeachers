namespace Data.Models
{
    public class StudentDTO : BaseEntity
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Course { get; set; }
        public int SchoolClassId { get; set; }
    }
}
