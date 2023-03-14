using Core.Repositories.Interfaces;
using Core.Services.Interfaces;

namespace Core.Services;

public class BookPhotoUploadService : IBookPhotoUploadService
{
    private readonly IBookPhotoRepository _bookPhotoRepository;

    public BookPhotoUploadService(IBookPhotoRepository bookPhotoRepository)
    {
        _bookPhotoRepository = bookPhotoRepository;
    }

    public async Task UploadAsync(
        Stream stream, 
        string contentType, 
        long length, 
        CancellationToken cancellationToken = default)
    {
        await _bookPhotoRepository.SaveAsync(
            stream, 
            contentType, 
            length, 
            cancellationToken);
    }
}
