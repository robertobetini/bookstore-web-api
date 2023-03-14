namespace Core.Repositories.Interfaces;

public interface IBookPhotoRepository
{
    Task<string> SavePhotoAsync(string bookId, Stream stream, string filename, string contentType, CancellationToken cancellationToken = default);
    Task DeletePhotoAsync(string photoId, CancellationToken cancellationToken = default);
    Task LinkPhotoToBookAsync(string bookId, string photoId, CancellationToken cancellationToken = default);
    Task UnlinkPhotoFromBookAsync(string bookId, string photoId, CancellationToken cancellationToken = default);
}
