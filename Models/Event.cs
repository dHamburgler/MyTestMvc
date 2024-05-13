using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EurofinsEvents.Models
{
    public class Event
    {
        [Key]
        public int Event_ID { get; set; }

        public string Title { get; set; }
        public string? EventImage { get; set; }
        public string Description { get; set; }
        public DateTime Datetime { get; set; }
        public string Location { get; set; }

        // add the date that the event is created
        // public DateTime Created { get; set; }
        //users to join button
        public bool Confirmed { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public ICollection<ApplicationUser> Guests { get; set; } = new List<ApplicationUser>();
        public ICollection<ApplicationUser> Votes { get; set; } = new List<ApplicationUser>();
    }
}
