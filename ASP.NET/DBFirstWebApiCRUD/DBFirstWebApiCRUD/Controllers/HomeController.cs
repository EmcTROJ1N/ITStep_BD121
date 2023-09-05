using System.Diagnostics;
using DBFirstWebApiCRUD.Data;
using Microsoft.AspNetCore.Mvc;
using DBFirstWebApiCRUD.Models;

namespace DBFirstWebApiCRUD.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly CarsContext _context;

    public HomeController(ILogger<HomeController> logger, CarsContext context)
    {
        _logger = logger;
        _context = context;
    }
    public IActionResult Index() => View();
    public IActionResult Privacy() => View();
    public IActionResult Cars() => View();
    
    public IActionResult EditCar(int id) => View(model:
        (from car in _context.CarsTables 
            where car.CarId == id
            select car).First());
    
    public IActionResult Owners() => View();
    public IActionResult EditOwner(int id) => View(model:
        (from owner in _context.OwnersTables 
            where owner.OwnerId == id
            select owner).First());

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}