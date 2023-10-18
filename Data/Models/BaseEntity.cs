using Data.Interfaces;

namespace Data.Models
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
