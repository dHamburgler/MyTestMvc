using Microsoft.AspNetCore.Mvc;
using EurofinsEvents.Models;
using EurofinsEvents.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;

namespace EurofinsEvents.Controllers
{
    [Authorize(Roles= "Admin")]
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IFileService _fileService;
        public EventController(IEventService eventService, IFileService fileService)
        {
            _eventService = eventService;
            _fileService = fileService;
        }
         //Index = eventList
        // GET: /Event
        public async Task<IActionResult> Index()
        {
            var events = await _eventService.GetEventsAsync();
            var viewModel = new ApplicationUser
            {
                EventList = events
            };
            return View(viewModel);
        }


        // GET: Event/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Title, Description, Datetime,  ImageFile, Location, Confirmed")] Event @event)
        {
            try
            {
                if (ModelState.IsValid)
                { // Check if an image file is uploaded
                    if (@event.ImageFile != null && @event.ImageFile.Length > 0)
                    {
                        // Save the image file
                        var fileResult =  _fileService.SaveImage(@event.ImageFile);
                        if (fileResult.Item1 == 0)
                        {
                            TempData["msg"] = "File could not be saved";
                            return View(@event);
                        }
                        // Set the image name to the event
                        var imageName = fileResult.Item2;
                      @event.EventImage = imageName;
                    }
                    else
                    {
                        @event.EventImage = "default.jpg";
                    }


                    // Call the service method to create the event
                    await _eventService.CreateEventAsync(@event);

                    // Redirect to the index action if successful
                    // After creating the event successfully
                    return RedirectToAction("Index", "Event");

                }
                // If model state is not valid, return to the create view
                return View(@event);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                ModelState.AddModelError("", "An error occurred while creating the event.");
                return View(@event);
            }
        }
        // GET: Event/EditList
        public async Task<IActionResult> EditList()
        {
            var events = await _eventService.GetEventsAsync();
            var viewModel = new ApplicationUser
            {
                EventList = events
            };
            return View(viewModel);
        }

        // GET: Event/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _eventService.GetEventByIdAsync(id.Value);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Event/Edit/{id}
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Event_ID, Title, Description, Datetime, ImageFile, Location, Confirmed")] Event @event)
        {
            
            if (ModelState.IsValid)
            {
                if (@event.ImageFile != null)
                {
                    var fileReult = this._fileService.SaveImage(@event.ImageFile);
                    if (fileReult.Item1 == 0)
                    {
                        TempData["msg"] = "File could not saved";
                        return View(@event);
                    }
                    var imageName = fileReult.Item2;
                    @event.EventImage = imageName;
                }
                else
                {
                    @event.EventImage = "default.jpg";
                }
                /*
                var result = _eventService.UpdateEventAsync(@event);
                if (result)            
                {
                    TempData["msg"] = "Added Successfully";
                    return RedirectToAction(nameof(EventListViewModel));
                }
                else
                {
                    TempData["msg"] = "Error on server side";
                    return View(@event);
                }*/
            }



            try
            {
                await _eventService.UpdateEventAsync(@event);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                ModelState.AddModelError("", "An error occurred while saving the event.");
                return View(@event);
            }
              

            return View(@event);
        }


        // GET: Event/Delete/{id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _eventService.GetEventByIdAsync(id.Value);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Event/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int Event_ID)
        {
            try
            {
                await _eventService.DeleteEventAsync(Event_ID);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return RedirectToAction(nameof(Delete), new { Event_ID, error = true });
            }
        }



    }

}
