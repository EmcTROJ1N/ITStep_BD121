using System.Diagnostics;
using System.Text.Json;
using System.Windows.Markup;
using Microsoft.AspNetCore.Mvc;
using FileExplorer.Models;

namespace FileExplorer.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private static string FolderPath;

    public HomeController(ILogger<HomeController> logger) =>
        _logger = logger;

    private List<FileModel> GetFiles(string? path = null)
    {
        if (path != null)
            HomeController.FolderPath = path;
        
        List<FileModel> files = new List<FileModel>();
        foreach (string fileName in Directory.GetFiles(HomeController.FolderPath).Concat(Directory.GetDirectories(HomeController.FolderPath)))
        {
            try { files.Add(new FileModel(fileName)); }
            catch { }
        }

        return files;
    }
    public IActionResult Index() => View();
    public IActionResult Privacy() => View();
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() =>
        View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

    public IActionResult EditFile(FileModel model) =>
        View(model: new FileEditViewModel(model));
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ShowFiles(string? path) =>
        View(this.GetFiles(path));

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SaveFile(FileEditViewModel model)
    {
        try
        {
            System.IO.File.WriteAllText(model.File.FullName, model.Text);
        }
        catch { return BadRequest(); }

        return RedirectToAction("Index");
    }
    
    public IActionResult CreateFile() => View();
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateFile(string fileName)
    {
        System.IO.File.Create($"{HomeController.FolderPath}/{fileName}");
        return PartialView("Table", this.GetFiles());
    }

    [HttpPost]
    public IActionResult DeleteFiles([FromBody] string[] filesArr)
    {
        try
        {
            foreach (string file in filesArr)
            {
                if (Directory.Exists(file))
                    Directory.Delete(file, true);
                else if (System.IO.File.Exists(file))
                    System.IO.File.Delete(file);
            }

            return Json(this.GetFiles(), new JsonSerializerOptions() { PropertyNamingPolicy = null });
        }
        catch { return BadRequest(); }
    }
    
    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFileCollection files)
    {
        try
        {
            if (ModelState.IsValid == false)
                throw new Exception();
            
            foreach (IFormFile file in files)
            {
                if (file == null)
                    continue;
                
                using (FileStream fileStream = new FileStream($"{HomeController.FolderPath}/{file.FileName}", FileMode.Create))
                    await file.CopyToAsync(fileStream);

            }
        }
        catch { return BadRequest(500); }
        return PartialView("Table", this.GetFiles());
    }
}