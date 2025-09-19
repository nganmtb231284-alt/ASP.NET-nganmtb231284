using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafeWebsite.Models;
using CafeWebsite.Data;

namespace CafeWebsite.Controllers;

public class HomeController : Controller
{
    private readonly CafeDbContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(CafeDbContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var featuredProducts = await _context.Products
            .Where(p => p.Available)
            .Take(8)
            .ToListAsync();
            
        var categoryCounts = await _context.Products
            .Where(p => p.Available)
            .GroupBy(p => p.Category)
            .Select(g => new { Category = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.Category, x => x.Count);
            
        var totalProducts = await _context.Products.CountAsync(p => p.Available);
        
        var viewModel = new HomeViewModel
        {
            FeaturedProducts = featuredProducts,
            CategoryCounts = categoryCounts,
            TotalProducts = totalProducts
        };
        
        return View(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
