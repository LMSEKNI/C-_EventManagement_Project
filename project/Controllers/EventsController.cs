using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class EventsController : Controller
{
    private readonly ApplicationDbContext _context;

    public EventsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: /Event
    // GET: /Event
    // GET: /Event
    public IActionResult Index(string date, string type, decimal? price)
    {
        // Retrieve the current user's ID from the session
        var userId = HttpContext.Session.GetInt32("UserId");

        // Get all events associated with the current user
        var eventsQuery = _context.Events
            .Where(e => e.UserId == userId);

        // Apply filters if provided
        if (!string.IsNullOrEmpty(date))
        {
            // Parse the date string
            if (DateTime.TryParse(date, out var filterDate))
            {
                // Filter events by date
                eventsQuery = eventsQuery.Where(e => e.DateDebut.Date == filterDate.Date);
            }
        }

        if (!string.IsNullOrEmpty(type))
        {
            // Filter events by type
            eventsQuery = eventsQuery.Where(e => e.Type == type);
        }

        if (price.HasValue)
        {
            // Filter events by price
            eventsQuery = eventsQuery.Where(e => decimal.Equals(e.Prix, price.Value));
        }

        // Include activities for the filtered events
        var events = eventsQuery.Include(e => e.Activities).ToList();

        return View(events);
    }



    // GET: /Event/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Event/Create
    // POST: /Event/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(EventViewModel model)
    {
        
            // Retrieve the current user's ID from the session
            var userId = HttpContext.Session.GetInt32("UserId");

            // Store the event data in the session
            HttpContext.Session.SetString("EventDateDebut", model.DateDebut.ToString());
            HttpContext.Session.SetString("EventDateFin", model.DateFin.ToString());
            HttpContext.Session.SetInt32("EventNombreMax", model.NombreMax);
            HttpContext.Session.SetString("EventDescription", model.Description);
            HttpContext.Session.SetString("EventType", model.Type);
            HttpContext.Session.SetString("EventNomEvent", model.NomEvent);
            HttpContext.Session.SetString("EventPrix", model.Prix.ToString());
            HttpContext.Session.SetInt32("EventUserId", userId.Value);

            return RedirectToAction("Create", "Activity");
        

        return View(model);
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var @event = _context.Events.FirstOrDefault(e => e.Id == id);

        if (@event == null)
        {
            return NotFound();
        }

        var model = new EventViewModel
        {
            Id = @event.Id,
            DateDebut = @event.DateDebut,
            DateFin = @event.DateFin,
            NombreMax = @event.NombreMax,
            Description = @event.Description,
            Type = @event.Type,
            NomEvent = @event.NomEvent,
            Prix = @event.Prix
        };

        return View(model);
    }

    // POST: Event/Edit/{id}
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, EventViewModel model)
    {
        

        var @event = _context.Events.FirstOrDefault(e => e.Id == id);

        if (@event == null)
        {
            return NotFound();
        }

        @event.DateDebut = model.DateDebut;
        @event.DateFin = model.DateFin;
        @event.NombreMax = model.NombreMax;
        @event.Description = model.Description;
        @event.Type = model.Type;
        @event.NomEvent = model.NomEvent;
        @event.Prix = model.Prix;

        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    // POST: Event/Delete/{id}
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var @event = _context.Events
            .Include(e => e.Activities) // Include the activities related to the event
            .FirstOrDefault(e => e.Id == id);

        if (@event == null)
        {
            return NotFound();
        }

        _context.Activities.RemoveRange(@event.Activities); // Remove the associated activities
        _context.Events.Remove(@event); // Remove the event itself
        _context.SaveChanges();

        return RedirectToAction("Index");
    }



}
