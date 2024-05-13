using System.Collections.Generic;
using EurofinsEvents.Models;
using Microsoft.AspNetCore.Identity;

namespace EurofinsEvents.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Event> EventList { get; set; } = new List<Event>();
        public ICollection<Event> Votes { get; set; } = new List<Event>();

    }
}

