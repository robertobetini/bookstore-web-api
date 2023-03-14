using Core.Repositories.Interfaces;

namespace Infrastructure.Repositories;

public class BookPhotoRepository : IBookPhotoRepository
{
    public Task SaveAsync(
        Stream stream, 
        string contentType, 
        long length, 
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
