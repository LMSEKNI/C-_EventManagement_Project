using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;



public class UserController : Controller
{
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }
    // GET: /User/Register
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    // POST: /User/Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Create a new User object and save it to the database
            var user = new User
            {
                Cin = model.Cin,
                Nom = model.Nom,
                Email = model.Email,
                Password = model.Password
            };

            // Save the user to the database using your DbContext
            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        return View(model);
    }

    // GET: /User/Login
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // POST: /User/Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Perform authentication and authorization checks
            // Example: Validate user credentials and set authentication cookie

            // Get the user by email and password from the database
            var user = _context.Users
                .FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

            if (user != null)
            {
                // Save the user id to the session
                HttpContext.Session.SetInt32("UserId", user.Id);

                return RedirectToAction("Index", "Events");
            }
        }

        return View(model);
    }
}
    
