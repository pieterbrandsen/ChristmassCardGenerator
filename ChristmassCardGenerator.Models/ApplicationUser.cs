using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ChristmassCardGenerator.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<EmailList> EmailLists { get; set; } 
    }

    public class EmailList
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ApplicationUser> List { get; set; }
    }
}
