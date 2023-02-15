namespace Core.Exceptions;

[Serializable]
public class BookNotFoundException : ResourceNotFoundException
{
	private const string RESOURCE = "Book";

	public BookNotFoundException() : base(RESOURCE) { }
}
