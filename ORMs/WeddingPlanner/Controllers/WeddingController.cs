using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers;

public class WeddingController : Controller
{
    private readonly ILogger<WeddingController> _logger;
    private readonly Context _context;

    public WeddingController(ILogger<WeddingController> logger, Context context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        // Restrict Access to registered users
        if (!HttpContext.Session.GetInt32("UserId").HasValue)
            return RedirectToAction("Index", "Auth");

        // Get current user
        User? currentUser = _context.Users.FirstOrDefault(u =>
            u.ID == HttpContext.Session.GetInt32("UserId")
        );

        List<Wedding> AllWedding = _context
            .Weddings.Include(w => w.Guests)
            .ThenInclude(a => a.Guest)
            .ToList();
        ViewBag.currentUser = currentUser;
        return View("Index", AllWedding);
    }

    [HttpGet("/weddings/{id}")]
    public IActionResult Details(int id)
    {
        // Restrict Access to registered users
        if (!HttpContext.Session.GetInt32("UserId").HasValue)
            return RedirectToAction("Index", "Auth");

        // Get current user
        User? currentUser = _context.Users.FirstOrDefault(u =>
            u.ID == HttpContext.Session.GetInt32("UserId")
        );
        ViewBag.currentUser = currentUser;

        // Get wedding and attendees
        Wedding? selected = _context
            .Weddings.Include(w => w.Guests)
            .ThenInclude(g => g.Guest)
            .FirstOrDefault(w => w.ID == id);

        return View("Details", selected);
    }

    [HttpGet("/weddings/plan")]
    public IActionResult PlanWedding()
    {
        // Restrict Access to registered users
        if (!HttpContext.Session.GetInt32("UserId").HasValue)
            return RedirectToAction("Index", "Auth");

        User? currentUser = _context.Users.FirstOrDefault(u =>
            u.ID == HttpContext.Session.GetInt32("UserId")
        );
        ViewBag.currentUser = currentUser;
        return View();
    }

    [HttpPost("/weddings/plan/submit")]
    public IActionResult AddWedding(Wedding newWedding)
    {
        // Restrict Access to registered users
        if (!HttpContext.Session.GetInt32("UserId").HasValue)
            return RedirectToAction("Index", "Auth");

        // Model-level Validation
        if (!ModelState.IsValid)
        {
            User? currentUser = _context.Users.FirstOrDefault(u =>
                u.ID == HttpContext.Session.GetInt32("UserId")
            );
            ViewBag.currentUser = currentUser;
            return View("PlanWedding");
        }

        // Wedding Date Validation
        if (newWedding.Date < DateTime.Today)
        {
            ModelState.AddModelError("Date", "Wedding date must be in the future");
            User? currentUser = _context.Users.FirstOrDefault(u =>
                u.ID == HttpContext.Session.GetInt32("UserId")
            );
            ViewBag.currentUser = currentUser;
            return View("PlanWedding");
        }

        // Save Wedding to DB
        _context.Weddings.Add(newWedding);
        _context.SaveChanges();
        return RedirectToAction("Details", new { id = newWedding.ID });
    }

    [HttpGet("/weddings/{id}/delete")]
    public IActionResult DeleteWedding(int id)
    {
        // Restrict Access to registered users
        if (!HttpContext.Session.GetInt32("UserId").HasValue)
            return RedirectToAction("Index", "Auth");

        Wedding? selected = _context.Weddings.FirstOrDefault(w => w.ID == id);
        _context.Weddings.Remove(selected);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpGet("/weddings/{id}/rsvp")]
    public IActionResult RSVP(int id)
    {
        // Restrict Access to registered users
        if (!HttpContext.Session.GetInt32("UserId").HasValue)
            return RedirectToAction("Index", "Auth");

        Attendance newAttendee = new Attendance
        {
            GuestId = HttpContext.Session.GetInt32("UserId").Value,
            WeddingId = id
        };

        // Save attendance to DB
        _context.GuestLists.Add(newAttendee);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    [HttpGet("/weddings/{id}/unrsvp")]
    public IActionResult UNRSVP(int id)
    {
        // Restrict Access to registered users
        if (!HttpContext.Session.GetInt32("UserId").HasValue)
            return RedirectToAction("Index", "Auth");

        int userId = HttpContext.Session.GetInt32("UserId").Value;
        Attendance selected = _context.GuestLists.FirstOrDefault(a =>
            a.GuestId == userId && a.WeddingId == id
        );

        // Save attendance to DB
        _context.GuestLists.Remove(selected);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }
}
