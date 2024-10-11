using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using INVENTORY_MANAGEMENT_SYSTEM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TVET_MANAGEMENT_SYSTEM.Data;

namespace INVENTORY_MANAGEMENT_SYSTEM.Controllers;
public class ReportController : Controller
{
    private readonly ApplicationDbContext _context;

    public ReportController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult StockReport()
    {
        var products = _context.Products
            .Select(p => new ProductReportViewModel
            {
                ProductName = p.ProductName,
                CurrentStock = p.CurrentStock,
                StockBalance = p.CurrentStock, // or whatever logic you have for stock balance
                ReorderLevel = p.ReorderLevel,
                StockMovements = _context.StockMovements
                    .Where(sm => sm.ProductId == p.ProductId)
                    .ToList()
            })
            .ToList();

        return View(products);
    }

}


