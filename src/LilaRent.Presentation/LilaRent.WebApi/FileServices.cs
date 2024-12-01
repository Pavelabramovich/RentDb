using LilaRent.Domain.Interfaces;


namespace LilaRent.WebApi;


public class FileService : IFileService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly string _directoryImagesPath;
    private readonly string _hostImagesPath;

    private const string FileDirectory = "Files";


    public FileService(
        IHttpContextAccessor httpContextAccessor,
        IWebHostEnvironment env)
    {
        _httpContextAccessor = httpContextAccessor;

        _directoryImagesPath = Path.Combine(env.WebRootPath, FileDirectory);
        var host = _httpContextAccessor.HttpContext!.Request.Host.ToString();

        if (host.Contains("localhost"))
        { 
            _hostImagesPath = $"https://{host}/{FileDirectory}";
        }
        else
        {
            _hostImagesPath = $"http://{host}/{FileDirectory}";
        }

        if (!Directory.Exists(_directoryImagesPath))
        {
            Directory.CreateDirectory(_directoryImagesPath);
        }
    }

    public Task<Stream> GetFileAsync(string filePath)
    {
        var fullPath = Path.Combine(_directoryImagesPath, filePath);

        if (!File.Exists(fullPath))
        {
            throw new FileNotFoundException(null, filePath);
        }

        return Task.FromResult((Stream)new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read));
    }

    public async Task<string> SaveFileAsync(Stream fileStream, string extension)
    {
        var newFileName = Guid.NewGuid().ToString();
        newFileName = Path.ChangeExtension(newFileName, extension);

        var newFilePath = Path.Combine(_directoryImagesPath, newFileName);

        using Stream newFileStream = new FileStream(newFilePath, FileMode.Create);
        await fileStream.CopyToAsync(newFileStream);

        return newFileName;
    }

    public Task DeleteFileAsync(string fileName)
    {
        var fullPath = Path.Combine(_directoryImagesPath, fileName);

        if (!File.Exists(fullPath))
        {
            throw new FileNotFoundException(fileName: fileName, message: null);
        }

        File.Delete(fullPath);

        return Task.CompletedTask;
    }

    public ValueTask<string> GetUri(string fileName)
    {
        return ValueTask.FromResult($"{_hostImagesPath}/{fileName}");
    }
}
