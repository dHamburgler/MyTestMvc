using EurofinsEvents.Models;

namespace EurofinsEvents.Repositories.Interface
{
    public interface IEventService
    {
        Task<List<Event>> GetEventsAsync();
        Task<Event> GetEventByIdAsync(int eventId);
        Task CreateEventAsync(Event newEvent);
        Task UpdateEventAsync(Event updatedEvent);
        Task DeleteEventAsync(int eventId);
        Task AddGuestToEvent(Event @event, ApplicationUser user);


    }
}

