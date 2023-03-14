namespace Core.Services.Interfaces;

public interface IBookPhotoService
{
    Task<byte[]> GetBookPhotoAsync(string bookId, CancellationToken cancellationToken = default);
    Task UploadPhotoAsync(string bookId, Stream stream, string fileName, string contentType, CancellationToken cancellationToken = default);
    Task RemovePhotoAsync(string bookId, string photoId, CancellationToken cancellationToken = default);
}
