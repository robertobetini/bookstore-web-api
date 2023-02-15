using Core.Entities;
using Core.Exceptions;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;

namespace Core.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<Book>> GetMany()
    {
        return await _bookRepository.GetMany();
    }

    public async Task<Book> GetOne()
    {
        var book = await _bookRepository.GetOne();

        if (book is null)
        {
            throw new BookNotFoundException();
        }

        return book;
    }
}
