using Core.Entities;

namespace Core.Repositories.Interfaces;

public interface IBookRepository
{
    Task<Book> GetOne();
    Task<IEnumerable<Book>> GetMany();
}
