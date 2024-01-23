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
    // var files = Directory.GetFiles(path).Select(f => Path.GetFileNameWithoutExtension(f)).ToArray();

        ViewBag.Files = files;


        return View();
    }

// [HttpGet("Content/{filename}")]
    public IActionResult Content(string filename)
    {
    try
        {

        var fileNameWithExtension = $"{filename}.txt";
        var filePath = Path.Combine(_env.ContentRootPath, "TextFiles", fileNameWithExtension);
        // var filePath = Path.Combine(_env.ContentRootPath, "TextFiles", filename + ".txt");

        // _logger.LogInformation("Attempting to display file at path: " + filePath);

        // if (System.IO.File.Exists(filePath))
        // {
        var content = System.IO.File.ReadAllText(filePath);
        ViewBag.Content = content;
        return View(model: content);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading file");
            return StatusCode(500, "An error occurred while reading the file.");
        }
        // }
        // else
        // {
        // _logger.LogWarning("File not found at path: " + filePath);
        // ViewBag.Error = "File not found.";
        // return View();
        // }
    }





}
