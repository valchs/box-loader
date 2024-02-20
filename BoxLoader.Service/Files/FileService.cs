using BoxLoader.Models.Boxes;
using BoxLoader.Service.Boxes;

namespace BoxLoader.Service.Files;

public class FileService : IFileService
{
	private readonly IBoxService _boxService;

	public FileService(IBoxService boxService)
	{
		_boxService = boxService ?? throw new ArgumentNullException(nameof(boxService));
	}

    public void MonitorDirectory(string path)
    {
        var fileWatcher = new FileSystemWatcher(path)
        {
            NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite
        };

        fileWatcher.Created += async (sender, e) =>
        {
            Console.WriteLine($"New file detected: {e.FullPath}");
            await Task.Delay(500);
            await ProcessFile(e.FullPath);
            Console.WriteLine($"Finished processing file");
        };

        fileWatcher.EnableRaisingEvents = true;
        Console.WriteLine($"Monitoring directory: {path}");
    }

    private async Task ProcessFile(string filePath)
    {
        BoxModel? box = null;

        foreach (var line in File.ReadLines(filePath))
        {
            if (line.StartsWith("HDR"))
            {
                // Reached a new box, save the current box in DB
                if (box != null) await _boxService.Upsert(box);

                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                box = new BoxModel
                {
                    SupplierIdentifier = parts[1].Trim(),
                    Identifier = parts[2].Trim(),
                };
            }
            else if (line.StartsWith("LINE") && box != null)
            {
                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var content = new ContentModel
                {
                    PoNumber = parts[1].Trim(),
                    Isbn = parts[2].Trim(),
                    Quantity = int.Parse(parts[3].Trim()),
                };
                box.Contents.Add(content);
            }
        }

        if (box != null) await _boxService.Upsert(box);
    }
}
