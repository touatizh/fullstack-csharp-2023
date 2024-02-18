using System.Diagnostics;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly Context _context;

    public HomeController(ILogger<HomeController> logger, Context context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("/")]
    public IActionResult Index()
    {
        ViewBag.Products = _context.Products.OrderBy(o => o.Name).Take(5).ToList();
        ViewBag.RecentOrders = _context
            .Orders.Include(o => o.Orderer)
            .Include(o => o.OrderedProduct)
            .OrderByDescending(o => o.CreatedAt)
            .Take(3)
            .ToList();
        ViewBag.RecentCustomers = _context
            .Customers.OrderByDescending(o => o.CreatedAt)
            .Take(3)
            .ToList();
        return View();
    }

    // * Customer routes
    [HttpGet("/customers")]
    public IActionResult Customers()
    {
        ViewBag.AllCustomers = _context.Customers.ToList();
        return View("Customers");
    }

    [HttpPost("/customers/add")]
    public IActionResult AddCustomer(Customer newCustomer)
    {
        // Model Validation
        if (!ModelState.IsValid)
        {
            ViewBag.AllCustomers = _context.Customers.ToList();
            return View("Customers");
        }

        // Save Changes
        _context.Customers.Add(newCustomer);
        _context.SaveChanges();

        return RedirectToAction("Customers");
    }

    [HttpGet("/customers/{id}/remove")]
    public IActionResult RemoveCustomer(int id)
    {
        // Get Customer
        Customer? selected = _context.Customers.FirstOrDefault(c => c.ID == id);

        // Save Changes
        _context.Customers.Remove(selected);
        _context.SaveChanges();

        return RedirectToAction("Customers");
    }

    // * Product routes
    [HttpGet("/products")]
    public IActionResult Products()
    {
        ViewBag.AllProducts = _context.Products.ToList();
        return View();
    }

    [HttpPost("/products/add")]
    public IActionResult AddProduct(Product newProduct)
    {
        // Model Validation
        if (!ModelState.IsValid)
        {
            ViewBag.AllProducts = _context.Products.ToList();
            return View("Products");
        }

        // Save Changes
        _context.Products.Add(newProduct);
        _context.SaveChanges();

        return RedirectToAction("Products");
    }

    // * Product routes
    [HttpGet("/orders")]
    public IActionResult Orders()
    {
        ViewBag.AllCustomers = _context.Customers.ToList();
        ViewBag.AllProducts = _context.Products.ToList();
        ViewBag.AllOrders = _context
            .Orders.Include(o => o.Orderer)
            .Include(o => o.OrderedProduct)
            .OrderByDescending(o => o.CreatedAt)
            .ToList();
        return View();
    }

    [HttpPost("/orders/add")]
    public IActionResult AddOrder(Order newOrder)
    {
        // Model Validation
        if (!ModelState.IsValid)
        {
            ViewBag.AllCustomers = _context.Customers.ToList();
            ViewBag.AllProducts = _context.Products.ToList();
            ViewBag.AllOrders = _context
                .Orders.Include(o => o.Orderer)
                .Include(o => o.OrderedProduct)
                .OrderByDescending(o => o.CreatedAt)
                .ToList();
            return View("Orders");
        }

        // Save Changes
        _context.Orders.Add(newOrder);
        Product ordered = _context.Products.FirstOrDefault(p => p.ID == newOrder.ProductId);
        if (ordered.Quatity - newOrder.Quantity > 0)
            ordered.Quatity -= newOrder.Quantity;
        else
            ordered.Quatity = 0;
        _context.SaveChanges();

        return RedirectToAction("Orders");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }
}
