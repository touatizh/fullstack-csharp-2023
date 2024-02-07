using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private DishesContext _context;

    public HomeController(ILogger<HomeController> logger, DishesContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        List<Dish> allDishes = _context.Dishes.ToList();
        return View("Index", allDishes);
    }

    [HttpGet("/dishes/new/")]
    public IActionResult NewDish()
    {
        return View("AddDish");
    }

    public IActionResult AddDish(Dish newDish)
    {
        if (!ModelState.IsValid)
            return View("AddDish");

        _context.Add(newDish);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpGet("/dishes/{id}")]
    public IActionResult DishDetails(int id)
    {
        Dish selectedDish = _context.Dishes.FirstOrDefault(d => d.DishId == id);
        return View("DishDetails", selectedDish);
    }

    [HttpGet("/dishes/{id}/edit")]
    public IActionResult EditDish(int id)
    {
        Dish selectedDish = _context.Dishes.FirstOrDefault(d => d.DishId == id);
        return View("EditDish", selectedDish);
    }

    public IActionResult UpdateDish(Dish updatedDish)
    {
        if (!ModelState.IsValid)
            return View("EditDish");

        Dish selectedDish = _context.Dishes.FirstOrDefault(d => d.DishId == updatedDish.DishId);
        selectedDish.Name = updatedDish.Name;
        selectedDish.Chef = updatedDish.Chef;
        selectedDish.Tastiness = updatedDish.Tastiness;
        selectedDish.Calories = updatedDish.Calories;
        selectedDish.Description = updatedDish.Description;

        _context.SaveChanges();
        return RedirectToAction("DishDetails", new { id = selectedDish.DishId });
    }

    [HttpGet("/dishes/{id}/delete")]
    public IActionResult DeleteDish(int id)
    {
        Dish selectedDish = _context.Dishes.FirstOrDefault(d => d.DishId == id);
        _context.Remove(selectedDish);
        _context.SaveChanges();
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
