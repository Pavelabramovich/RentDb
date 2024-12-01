namespace LilaRent.Domain.Interfaces;


public interface IFileService
{
    Task<string> SaveFileAsync(Stream fileStream, string extension);
    Task DeleteFileAsync(string fileName);
    Task<Stream> GetFileAsync(string fileName);

    ValueTask<string> GetUri(string fileName);
}
