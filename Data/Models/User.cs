namespace Data.Models
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Number { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public int YearOfBirth { get; set; }
        public int MonthOfBirth { get; set; }
        public int DayOfBirth { get; set; }
        public int Age { get; set; }
        public bool IsAdult { get; set; }
    }
}
