using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using INVENTORY_MANAGEMENT_SYSTEM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TVET_MANAGEMENT_SYSTEM.Data;

namespace INVENTORY_MANAGEMENT_SYSTEM.Controllers;

public class HomeController : Controller
{   private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
     private readonly UserManager<ApplicationUser> _userManager;

    public HomeController(ILogger<HomeController> logger,  ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
    
    var products = _context.Products.ToList();
    var lowStockProducts = _context.Products.Where(p => p.CurrentStock < p.ReorderLevel).ToList();

        // Pass lowStockProducts to the view using ViewBag
    ViewBag.LowStockProducts = lowStockProducts;
    return View(products);
    }

    // Add New Product
    public IActionResult AddProduct()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddProduct(Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(product);
    }

    // Manage Stock Movements
    public IActionResult StockMovement()
    {
        var products = _context.Products.ToList();
        return View(products);
    }

    [HttpPost]
    public IActionResult StockMovement(int productId, int quantity, string movementType, string enteredBy)
    {
        var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
        if (product != null)
        {
            if (movementType == "Sales" || movementType == "Swap" || movementType == "Replacement")
            {
                product.CurrentStock -= quantity;
            }
            else if (movementType == "Arrival")
            {
                product.CurrentStock += quantity;
            }

            _context.StockMovements.Add(new StockMovement
            {
                ProductId = productId,
                EnteredBy = enteredBy,
                Quantity = quantity,
                MovementType = movementType,
                MovementDate = DateTime.UtcNow // Updated this line
                
            });

            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    // Stock Movement Report
    public IActionResult Report()
    {
        var movements = _context.StockMovements.Include(sm => sm.Product).ToList();
        return View(movements);
    }

    // Alert for Low Stock Levels
    // public IActionResult CheckReorderLevels()
    // {
    //     var lowStockProducts = _context.Products.Where(p => p.CurrentStock < p.ReorderLevel).ToList();
    //     // Pass both products and lowStockProducts to the view
    //     ViewBag.LowStockProducts = lowStockProducts;
    //     return View(lowStockProducts);
    // }

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
