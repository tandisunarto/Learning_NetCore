namespace MVCLesson.Models
{
    public class AppInfoSettings
    {
        public string Name { get; set; }
        public Address Location { get; set; }
        public int MaxPriority { get; set; }
        public bool IsDone { get; set; }
    }

    public class Address {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}