using Core.Entities;

namespace Core.Services.Interfaces;

public interface IBookService
{
    Task<Book> GetOne();
    Task<IEnumerable<Book>> GetMany();
}
