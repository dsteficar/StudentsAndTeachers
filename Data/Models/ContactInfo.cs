namespace Data.Models
{
    public class ContactInfo : BaseEntity
    {
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? WebsiteLink { get; set; }
        public string? ContactNumber { get; set; }
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; } = null!;

    }
}
