using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsCategories.Models;

namespace ProductsCategories.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly Context _context;

    public HomeController(ILogger<HomeController> logger, Context context)
    {
        _logger = logger;
        _context = context;
    }

    // Products
    public IActionResult Index()
    {
        ViewBag.Products = _context.Products.ToList();
        return View();
    }

    [HttpPost]
    public IActionResult AddProduct(Product newProduct)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Products = _context.Products.ToList();
            return View("Index");
        }

        _context.Products.Add(newProduct);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    
    [HttpGet("products/{id}")]
    public IActionResult ProductDetails(int id)
    {
        Product selected = _context.Products.Include(p => p.Categories).ThenInclude(c => c.Category).FirstOrDefault(p => p.ID == id);

        ViewBag.AllCategories = _context.Categories.ToList();
        return View("ProductDetails", selected);
    }

    // Categories
    public IActionResult Categories()
    {
        ViewBag.Categories = _context.Categories.ToList();
        return View();
    }

    [HttpPost]
    public IActionResult AddCategory(Category newCategory)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View("Categories");
        }

        _context.Categories.Add(newCategory);
        _context.SaveChanges();
        return RedirectToAction("Categories");
    }

    [HttpGet("categories/{id}")]
    public IActionResult CategoryDetails(int id)
    {
        Category selected = _context.Categories.Include(c => c.Products).ThenInclude(p => p.Product).FirstOrDefault(c => c.ID == id);

        ViewBag.AllProducts = _context.Products.ToList();
        return View("CategoryDetails", selected);
    }

    // Add Association
    public IActionResult AddAssociation(Association newAssociation)
    {
        if(!ModelState.IsValid)
        {
            Product selected = _context.Products.Include(p => p.Categories).ThenInclude(c => c.Category).FirstOrDefault(p => p.ID == newAssociation.ProductId);
            ViewBag.AllCategories = _context.Categories.ToList();
            return View("ProductDetails", selected);
        }

        _context.Associations.Add(newAssociation);
        _context.SaveChanges();
        return RedirectToAction("ProductDetails", new { id = newAssociation.ProductId });
    }

    public IActionResult CategoryAssociation(Association newAssociation)
    {
        if(!ModelState.IsValid)
        {
            Category selected = _context.Categories.Include(c => c.Products).ThenInclude(p => p.Product).FirstOrDefault(c => c.ID == newAssociation.CategoryId);
            ViewBag.AllProducts = _context.Products.ToList();
            return View("CategoryDetails", selected);
        }

        _context.Associations.Add(newAssociation);
        _context.SaveChanges();
        return RedirectToAction("CategoryDetails", new { id = newAssociation.CategoryId });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
