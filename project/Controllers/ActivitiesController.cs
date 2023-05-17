using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

public class ActivityController : Controller
{
    private readonly ApplicationDbContext _context;

    public ActivityController(ApplicationDbContext context)
    {
        _context = context;
    }


    // GET: /Activity/Create
    [HttpGet]
    public IActionResult Create()
    {
        // Retrieve event details from session or database
        // Retrieve event details from session or database
        var eventDateDebut = HttpContext.Session.GetString("EventDateDebut");
        var eventDateFin = HttpContext.Session.GetString("EventDateFin");
        var eventNombreMax = HttpContext.Session.GetInt32("EventNombreMax");
        var eventDescription = HttpContext.Session.GetString("EventDescription");
        var eventType = HttpContext.Session.GetString("EventType");
        var eventNomEvent = HttpContext.Session.GetString("EventNomEvent");
        var eventPrix = HttpContext.Session.GetString("EventPrix");

        // Create the Event object
        var eventModel = new Event
        {
            DateDebut = DateTime.Parse(eventDateDebut),
            DateFin = DateTime.Parse(eventDateFin),
            NombreMax = eventNombreMax.Value,
            Description = eventDescription,
            Type = eventType,
            NomEvent = eventNomEvent,
            Prix = float.Parse(eventPrix)
        };

        // Convert the Event object to EventViewModel
        var eventViewModel = new EventViewModel
        {
            DateDebut = eventModel.DateDebut,
            DateFin = eventModel.DateFin,
            NombreMax = eventModel.NombreMax,
            Description = eventModel.Description,
            Type = eventModel.Type,
            NomEvent = eventModel.NomEvent,
            Prix = eventModel.Prix
        };

        // Create the ActivityCreateViewModel and assign the converted EventViewModel
        var viewModel = new ActivityCreateViewModel
        {
            Event = eventViewModel
        };

        return View(viewModel);

    }

    // POST: /Activity/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ActivityCreateViewModel model)
    {
       
            // Retrieve the event details from the session
            var eventDateDebut = HttpContext.Session.GetString("EventDateDebut");
            var eventDateFin = HttpContext.Session.GetString("EventDateFin");
            var eventNombreMax = HttpContext.Session.GetInt32("EventNombreMax");
            var eventDescription = HttpContext.Session.GetString("EventDescription");
            var eventType = HttpContext.Session.GetString("EventType");
            var eventNomEvent = HttpContext.Session.GetString("EventNomEvent");
            var eventPrix = HttpContext.Session.GetString("EventPrix");
            var eventUserId = HttpContext.Session.GetInt32("EventUserId");

            // Create the event object
            var newEvent = new Event
            {
                DateDebut = DateTime.Parse(eventDateDebut),
                DateFin = DateTime.Parse(eventDateFin),
                NombreMax = eventNombreMax.Value,
                Description = eventDescription,
                Type = eventType,
                NomEvent = eventNomEvent,
                Prix = float.Parse(eventPrix),
                UserId = eventUserId.Value
            };

            // Save the event to the database
            _context.Events.Add(newEvent);
            _context.SaveChanges();

            // Create the activities and associate them with the event
            for (int i = 0; i < model.NumberOfActivities; i++)
            {
                var activity = new Activity
                {
                    HeureDebut = TimeSpan.Parse(model.Activities[i].HeureDebut),
                    HeureFin = TimeSpan.Parse(model.Activities[i].HeureFin),

                    NomActivity = model.Activities[i].NomActivity,
                    Description = model.Activities[i].Description,
                    Date = model.Activities[i].Date,
                    EventId = newEvent.Id
                };

                _context.Activities.Add(activity);
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Events");
        

        return View(model);
    }
    // GET: /Activity/Edit/{id}
    // GET: /Activity/Edit/{id}
   

}
