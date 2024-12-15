using LilaRent.Domain.Interfaces;


namespace LilaRent.Application.Tests.Fakes;


internal class FakeFileService : IFileService
{
    private readonly string _serverPrefix;
    private readonly Dictionary<string, Stream> _files;


    public FakeFileService(string serverPrefix, Dictionary<string, Stream> files)
    {
        _serverPrefix = serverPrefix;
        _files = files;
    }


    public Task<Stream> GetFileAsync(string fileName)
    {
        return Task.FromResult(_files[fileName]);
    }

    public Task<string> SaveFileAsync(Stream fileStream, string extension)
    {
        var newFileName = Path.ChangeExtension(Guid.NewGuid().ToString(), extension);
        _files[newFileName] = fileStream;
        return Task.FromResult(newFileName);
    }

    public Task DeleteFileAsync(string fileName)
    {
        _files.Remove(fileName);
        return Task.CompletedTask;
    }

    public ValueTask<string> GetUri(string fileName)
    {
        return ValueTask.FromResult($"{_serverPrefix}/{fileName}"); 
    }
}
