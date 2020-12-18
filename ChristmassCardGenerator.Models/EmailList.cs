namespace ChristmassCardGenerator.Models
{
    public enum ContactTypes
    {
        None, Family, Friend, Colleague
    }
    public class EmailList
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ContactTypes ContactType { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
