namespace ChristmassCardGenerator.Models
{
    public class EmailList
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
