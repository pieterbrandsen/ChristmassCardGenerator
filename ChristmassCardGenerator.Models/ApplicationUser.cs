using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;


namespace ChristmassCardGenerator.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public virtual ICollection<Card> Cards { get; set; }
    }
}
