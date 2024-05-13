using EurofinsEvents.Data;
using EurofinsEvents.Models;
using EurofinsEvents.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace EurofinsEvents.Repositories.Implemntation
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _dbContext;

        public EventService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Event>> GetEventsAsync()
        {
            return await _dbContext.Events
                .Include(x => x.Guests)
                .Include(x => x.Votes)
                .ToListAsync();
        }


        public async Task<Event> GetEventByIdAsync(int eventId)
        {
            return await _dbContext.Events.FirstOrDefaultAsync(e => e.Event_ID == eventId);
        }

        public async Task CreateEventAsync(Event newEvent)
        {
            _dbContext.Events.Add(newEvent);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateEventAsync(Event updatedEvent)
        {
            _dbContext.Entry(updatedEvent).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteEventAsync(int eventId)
        {
            var eventToDelete = await _dbContext.Events.FindAsync(eventId);
            if (eventToDelete != null)
            {
                _dbContext.Events.Remove(eventToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task AddGuestToEvent(Event @event, ApplicationUser user)
        {
            // check if the guest already has joined the group
            var exists = _dbContext.Events.Any(x => x.Event_ID == @event.Event_ID && x.Guests.Any(g => g.Id == user.Id));
            if (exists == false)
            {
                @event.Guests.Add(user);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
