namespace Data.Models
{
    public class SchoolClassDTO : BaseEntity
    {
        public string? Name { get; set; }
        public int StudentCapacity { get; set; }
        public bool Online { get; set; }
    }
}
