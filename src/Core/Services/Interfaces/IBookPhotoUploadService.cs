namespace Core.Services.Interfaces;

public interface IBookPhotoUploadService
{
    Task UploadAsync(Stream stream, string contentType, long length, CancellationToken cancellationToken = default);
}
