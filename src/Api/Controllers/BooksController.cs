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
    public async Task<IEnumerable<Book>> GetMany()
    {
        return await _bookService.GetMany();
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<Book> GetOne()
    {
        return await _bookService.GetOne();
    }
}