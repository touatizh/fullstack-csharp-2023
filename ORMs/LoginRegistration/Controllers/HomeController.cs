using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using LoginRegistration.Models;

namespace LoginRegistration.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private UserContext _context;

    public HomeController(ILogger<HomeController> logger, UserContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("/register")]
    public IActionResult Register(User user)
    {
        if (!ModelState.IsValid)
            return View("Index");

        if (_context.Users.Any(u => u.Email == user.Email))
        {
            ModelState.AddModelError("Email", "Email already in use!");
            return View("Index");
        }

        PasswordHasher<User> Hasher = new PasswordHasher<User>();
        user.Password = Hasher.HashPassword(user, user.Password);
        _context.Users.Add(user);
        _context.SaveChanges();

        HttpContext.Session.SetInt32("UserId", user.ID);
        return RedirectToAction("Success");
    }

    public IActionResult Login(LoginUser loggedUser)
    {
        if (!ModelState.IsValid)
            return View("Index");

        User? userInDb = _context.Users.FirstOrDefault(u => u.Email == loggedUser.LoginEmail);
        if (userInDb == null)
        {
            ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
            return View("Index");
        }

        PasswordHasher<LoginUser> Hasher = new PasswordHasher<LoginUser>();
        var isValidPassword = Hasher.VerifyHashedPassword(loggedUser, userInDb.Password, loggedUser.LoginPassword);
        if (isValidPassword == 0)
        {
            ModelState.AddModelError("LoginPassword", "Invalid Email/Password");
            return View("Index");
        }

        HttpContext.Session.SetInt32("UserId", userInDb.ID);
        return RedirectToAction("Success");
    }

    [HttpGet("/success")]
    public IActionResult Success()
    {
        if (!HttpContext.Session.GetInt32("UserId").HasValue)
            return RedirectToAction("Index");

        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
