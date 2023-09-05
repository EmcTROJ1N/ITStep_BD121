using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FileExplorerWebApi.Models;

namespace FileExplorerWebApi.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult GetFolderPath()
    {
        return View();
    }

    public IActionResult ShowFolder(string folderPath)
    {
        return View(model: folderPath);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}