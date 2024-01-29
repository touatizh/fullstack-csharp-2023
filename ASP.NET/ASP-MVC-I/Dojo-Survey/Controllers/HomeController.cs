using Microsoft.AspNetCore.Mvc;
namespace DojoSurvey.Controllers;
using DojoSurvey.Models;

public class HomeController : Controller
{
    public static SurveyForm userSurvey = new SurveyForm();
    [HttpGet("")]
    public ViewResult Index()
    {
        return View();
    }

    [HttpPost("submit")]
    public IActionResult Submit(SurveyForm survey)
    {
        if (!ModelState.IsValid)
            return View("Index");

        userSurvey = survey;
        return RedirectToAction("SurveyInfo");
    }

    [HttpGet("survey")]
    public IActionResult SurveyInfo()
    {
        return View("SurveyInfo", userSurvey);
    }
}