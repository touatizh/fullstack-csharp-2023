using Microsoft.AspNetCore.Mvc;
namespace DojoSurvey.Controllers;
using DojoSurvey.Models;

public class HomeController : Controller
{
    [HttpGet("")]
    public ViewResult Index()
    {
        return View();
    }

    [HttpPost("submit")]
    public IActionResult Submit(SurveyForm survey)
    {
        TempData["form_data"] = new Dictionary<string, string> {
            {"name", survey.name},
            {"location", survey.location},
            {"language", survey.language},
            {"comments", survey.comments}
        };
        return RedirectToAction("SurveyInfo");
    }

    [HttpGet("survey")]
    public IActionResult SurveyInfo()
    {
        var survey_data = TempData["form_data"] as Dictionary<string, string>;
        SurveyForm survey = new SurveyForm()
        {
            name = survey_data["name"],
            location = survey_data["location"],
            language = survey_data["language"],
            comments = survey_data["comments"],
        };
        return View("SurveyInfo", survey);
    }
}