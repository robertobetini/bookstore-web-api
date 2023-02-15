using Core.Entities;
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
        return await _bookRepository.GetOne();
    }
}
