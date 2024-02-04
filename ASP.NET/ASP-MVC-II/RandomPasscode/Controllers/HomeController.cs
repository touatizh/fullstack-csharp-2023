using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RandomPasscode.Models;
using Microsoft.AspNetCore.Http;

namespace RandomPasscode.Controllers;

public class HomeController : Controller
{
    public int passcodeCount;
    private readonly ILogger<HomeController> _logger;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult GeneratePasscode()
    {
        string allowedChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        Random rand = new Random();
        string passcode = "";
        passcodeCount = HttpContext.Session.GetInt32("passCount") ?? 0;
        for (int i = 0; i < 14; i++)
        {
            int randomCharIndex = rand.Next(allowedChars.Length);
            passcode += allowedChars[randomCharIndex];
        }
        passcodeCount++;

        HttpContext.Session.SetString("passcode", passcode);
        HttpContext.Session.SetInt32("passCount", passcodeCount);
        return RedirectToAction("Index");
    }

    public IActionResult Reset()
    {
        HttpContext.Session.Clear();
        return View("Index");
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
