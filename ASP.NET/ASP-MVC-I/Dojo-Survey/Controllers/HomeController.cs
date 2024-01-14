using Microsoft.AspNetCore.Mvc;
namespace DojoSurvey.Controllers;

public class HomeController : Controller
{
    [HttpGet("")]
    public ViewResult Index()
    {
        return View();
    }

    [HttpPost("submit")]
    public IActionResult Index(string name, string location, string language, string comments)
    {
        TempData["form_data"] = new Dictionary<string, string> {
            {"name", name},
            {"location", location},
            {"language", language},
            {"comments", comments}
        };

        return RedirectToAction("SurveyInfo");
    }

    [HttpGet("survey")]
    public IActionResult SurveyInfo()
    {
        ViewBag.Context = TempData["form_data"];
        return View();
    }
}