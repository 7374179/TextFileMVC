using Microsoft.AspNetCore.Mvc;

namespace TextFilesMVC;

public class FileController : Controller
{
    private readonly ILogger<FileController> _logger;
    private readonly IWebHostEnvironment _env;

    public FileController(ILogger<FileController> logger, IWebHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public IActionResult Index()
    {
        // var path = Path.Combine(_env.ContentRootPath, "TextFiles");
        // var files = Directory.GetFiles(path);

        // ViewBag.Files = files.Select(f => Path.GetFileName(f)).ToArray();

        var path = Path.Combine(_env.ContentRootPath, "TextFiles");
        var files = Directory.GetFiles(path)
                             .Select(Path.GetFileName)
                             .OrderBy(f => f) // sort in ascending order
                             .ToArray();

        ViewBag.Files = files;


        return View();
    }

    public IActionResult Display(string filename)
    {
        var fileNameWithExtension = $"{filename}.txt";
        var filePath = Path.Combine(_env.ContentRootPath, "TextFiles", fileNameWithExtension);

        // _logger.LogInformation("Attempting to display file at path: " + filePath);

        // if (System.IO.File.Exists(filePath))
        // {
        var content = System.IO.File.ReadAllText(filePath);
        ViewBag.Content = content;
        return View();
        // }
        // else
        // {
        // _logger.LogWarning("File not found at path: " + filePath);
        // ViewBag.Error = "File not found.";
        // return View();
        // }
    }





}
