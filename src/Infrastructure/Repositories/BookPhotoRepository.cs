using Core.Repositories.Interfaces;
using Infrastructure.DbContexts.MongoDB.Interfaces;
using Infrastructure.DbEntities.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace Infrastructure.Repositories;

public class BookPhotoRepository : IBookPhotoRepository
{
    private readonly IGridFSBucket _gridFSBucket;
    private readonly IMongoCollection<BookMongoDB> _collection;
    private readonly FilterDefinitionBuilder<BookMongoDB> _bookFilter = Builders<BookMongoDB>.Filter;
    private readonly UpdateDefinitionBuilder<BookMongoDB> _bookUpdate = Builders<BookMongoDB>.Update;

    public BookPhotoRepository(IBookstoreMongoDBContext context)
    {
        _gridFSBucket = new GridFSBucket(context.Database);
        _collection = context.Database.GetCollection<BookMongoDB>("books");
    }

    public async Task<string> SavePhotoAsync(
        string bookId,
        Stream stream,
        string fileName,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        var options = new GridFSUploadOptions
        {
            Metadata = new()
            {
                { "bookId", bookId },
                { "contentType", contentType }
            }
        };

        var photoId = await _gridFSBucket.UploadFromStreamAsync(
            fileName, 
            stream,
            options,
            cancellationToken);

        return photoId.ToString();
    }

    public async Task DeletePhotoAsync(
        string photoId, 
        CancellationToken cancellationToken = default)
    {
        await _gridFSBucket.DeleteAsync(ObjectId.Parse(photoId), cancellationToken);
    }

    public async Task LinkPhotoToBookAsync(
        string bookId, 
        string photoId, 
        CancellationToken cancellationToken = default)
    {
        var filter = _bookFilter.Eq(book => book.Id, bookId);

        var update = _bookUpdate.Push(book => book.PhotoIds, photoId);

        await _collection.UpdateOneAsync(
            filter, 
            update, 
            cancellationToken: cancellationToken);
    }

    public async Task UnlinkPhotoFromBookAsync(
        string bookId, 
        string photoId, 
        CancellationToken cancellationToken = default)
    {
        var filter = _bookFilter.Eq(book => book.Id, bookId);

        var update = _bookUpdate.Pull(book => book.PhotoIds, photoId);

        await _collection.UpdateOneAsync(
            filter,
            update,
            cancellationToken: cancellationToken);
    }
}
