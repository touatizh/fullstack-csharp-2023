using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChefsDishes.Models;

namespace ChefsDishes.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private ChefDishContext _context;

    public HomeController(ILogger<HomeController> logger, ChefDishContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Chefs()
    {
        ViewBag.AllChefs = _context.Chefs.Include(c => c.CreatedDishes).ToList(); ;
        return View();
    }

    // Chef-related routes
    [HttpGet("chefs/new")]
    public IActionResult NewChef()
    {
        return View();
    }

    [HttpPost("chefs/new/process")]
    public IActionResult AddChef(Chef newChef)
    {
        if (!ModelState.IsValid)
            return View("NewChef");

        if (newChef.DateOfBirth >= DateTime.Now.AddYears(-18))
        {
            ModelState.AddModelError("DateOfBirth", "Chef must be at least 18 years old.");
            return View("NewChef");
        }

        _context.Chefs.Add(newChef);
        _context.SaveChanges();

        return RedirectToAction("Chefs");
    }

    // Dish-related routes
    [HttpGet("dishes")]
    public IActionResult Dishes()
    {
        ViewBag.AllDishes = _context.Dishes.Include(d => d.Chef).ToList();
        return View();
    }

    [HttpGet("dishes/new")]
    public IActionResult NewDish()
    {
        ViewBag.AllChefs = _context.Chefs.ToList(); ;
        return View();
    }

    [HttpPost("dishes/new/process")]
    public IActionResult AddDish(Dish newDish)
    {
        if (!ModelState.IsValid)
            return View("NewDish");

        _context.Dishes.Add(newDish);
        _context.SaveChanges();

        return RedirectToAction("Dishes");
    }

    //********************************************************
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
