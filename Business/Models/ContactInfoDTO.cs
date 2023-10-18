namespace Data.Models
{
    public class ContactInfoDTO : BaseEntity
    {
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? WebsiteLink { get; set; }
        public string? ContactNumber { get; set; }
        public int TeacherId { get; set; }

    }
}
