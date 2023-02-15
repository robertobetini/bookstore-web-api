namespace Core.Exceptions;

[Serializable]
public class BookAlreadyDeletedException : ResourceAlreadyDeletedException
{
    private const string RESOURCE = "Book";

    public BookAlreadyDeletedException() : base(RESOURCE) { }
}