using Core.Exceptions;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;

namespace Core.Services;

public class BookPhotoService : IBookPhotoService
{
    private readonly IEnumerable<string> _validImageTypes = new string[]
    {
        "image/jpeg",
        "image/png"
    };

    private readonly IBookPhotoRepository _bookPhotoRepository;

    public BookPhotoService(IBookPhotoRepository bookPhotoRepository)
    {
        _bookPhotoRepository = bookPhotoRepository;
    }

    public async Task UploadPhotoAsync(
        string bookId,
        Stream stream,
        string filename,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        if (!_validImageTypes.Any(validType => validType == contentType))
        {
            throw new InvalidImageTypeException(contentType);
        }

        var photoId = await _bookPhotoRepository.SavePhotoAsync(
            bookId,
            stream,
            filename,
            contentType,
            cancellationToken);

        await _bookPhotoRepository.LinkPhotoToBookAsync(bookId, photoId, cancellationToken);
    }

    public async Task RemovePhotoAsync(
        string bookId, 
        string photoId, 
        CancellationToken cancellationToken = default)
    {
        await _bookPhotoRepository.DeletePhotoAsync(photoId, cancellationToken);
        await _bookPhotoRepository.UnlinkPhotoFromBookAsync(bookId, photoId, cancellationToken);
    }
}
