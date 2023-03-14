namespace Core.Repositories.Interfaces;

public interface IBookPhotoRepository
{
    Task SaveAsync(Stream stream, string contentType, long length, CancellationToken cancellationToken = default);
}
