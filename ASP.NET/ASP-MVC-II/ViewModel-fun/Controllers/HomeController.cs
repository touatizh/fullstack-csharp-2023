using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ViewModel_fun.Models;

namespace ViewModel_fun.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        string message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        return View("Index", message);
    }

    [HttpGet("numbers")]
    public IActionResult Numbers()
    {
        int[] nbrArray = new int[] { 37, 50, 7, 22, 100, 6, 15, 28 };
        return View("Numbers", nbrArray);
    }

    [HttpGet("users")]
    public IActionResult Users()
    {
        List<User> usersList = new List<User>();
        usersList.Add(new User() { FirstName = "Tim", LastName = "Corey" });
        usersList.Add(new User() { FirstName = "Sue", LastName = "Storm" });
        usersList.Add(new User() { FirstName = "Joe", LastName = "Doe" });
        usersList.Add(new User() { FirstName = "Sarah", LastName = "Smith" });
        return View("Users", usersList);
    }

    [HttpGet("user")]
    public IActionResult User()
    {
        User newUser = new User() { FirstName = "Edith", LastName = "Blair" };
        return View("User", newUser);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
