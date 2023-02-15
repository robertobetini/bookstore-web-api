using Core.Entities;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IEnumerable<Book>> GetMany(CancellationToken cancellationToken)
    {
        return await _bookService.GetManyAsync(cancellationToken);
    }

    [HttpGet]
    [Route("{bookId}")]
    public async Task<Book> GetOne([FromRoute] string bookId, CancellationToken cancellationToken)
    {
        return await _bookService.GetOneAsync(bookId, cancellationToken);
    }

    [HttpPost]
    public async Task<string> Create([FromBody] Book book, CancellationToken cancellationToken)
    {
        return await _bookService.CreateAsync(book, cancellationToken);
    }

    [HttpPut]
    [Route("{bookId}")]
    public async Task Update(
        [FromRoute] string bookId, 
        [FromBody] Book book, 
        CancellationToken cancellationToken)
    {
        await _bookService.UpdateAsync(bookId, book, cancellationToken);
    }

    [HttpDelete]
    [Route("{bookId}")]
    public async Task Delete(
        [FromRoute] string bookId,
        CancellationToken cancellationToken)
    {
        await _bookService.DeleteAsync(bookId, cancellationToken);
    }
}