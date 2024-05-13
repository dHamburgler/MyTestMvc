using EurofinsEvents.Data;
using EurofinsEvents.Models;
using Microsoft.AspNetCore.Mvc;
using EurofinsEvents.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace EurofinsEvents.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEventService _eventService;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IEventService eventService, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _eventService = eventService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }

        public async Task<IActionResult> Occasion()
        {
            var events = await _eventService.GetEventsAsync();
            return View(events);
        }
        
        public async Task<IActionResult> Join(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // look up @event wich reprsent Events in the db
            var @event = await _eventService.GetEventByIdAsync(id.Value);
            if (@event == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            await _eventService.AddGuestToEvent(@event, user);

            return RedirectToAction(nameof(Occasion));
        }
        
        public async Task<IActionResult> Unjoin(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var @event = await _context.Events.Include(x => x.Guests).FirstAsync(x => x.Event_ID == id);
            if (@event == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            // check if the guest already has joined the group
            var exists = _context.Events.Any(x => x.Event_ID == @event.Event_ID && x.Guests.Any(g => g.Id == user.Id));
            if (exists)
            {
                @event.Guests.Remove(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Occasion));
        }


        public async Task<IActionResult> Vote(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // look up @event wich reprsent Events in the db
            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            // get user from DB
            var user = await _userManager.GetUserAsync(User);

            // check if the guest already has joined the group
            var exists = _context.Events.Any(x => x.Event_ID == @event.Event_ID && x.Votes.Any(g => g.Id == user.Id));
            if (exists == false)
            {
                @event.Votes.Add(user);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Occasion));
        }

        public async Task<IActionResult> Unvote(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var @event = await _context.Events.Include(x => x.Votes).FirstAsync(x => x.Event_ID == id);
            if (@event == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            // check if the guest already has joined the group
            var exists = _context.Events.Any(x => x.Event_ID == @event.Event_ID && x.Votes.Any(g => g.Id == user.Id));
            if (exists)
            {
                @event.Votes.Remove(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Occasion));
        }
    }
}


        

  
  