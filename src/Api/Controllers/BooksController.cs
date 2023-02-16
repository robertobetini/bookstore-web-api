using Api.Adapters;
using Api.DTOs.Request;
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
    public async Task<IActionResult> GetMany(CancellationToken cancellationToken)
    {
        var books = await _bookService.GetManyAsync(cancellationToken: cancellationToken);
        var response = BookAdapter.ToGetBookDTOs(books);
        return Ok(response);
    }

    [HttpGet]
    [Route("{bookId}")]
    public async Task<IActionResult> GetOne([FromRoute] string bookId, CancellationToken cancellationToken)
    {
        var book = await _bookService.GetOneAsync(bookId, cancellationToken: cancellationToken);
        var response = BookAdapter.ToGetBookDTO(book);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookDTO bookDTO, CancellationToken cancellationToken)
    {
        var book = BookAdapter.ToDomainEntity(bookDTO);
        var createdBookId = await _bookService.CreateAsync(book, cancellationToken);
        return Ok(createdBookId);
    }

    [HttpPatch]
    [Route("{bookId}")]
    public async Task<IActionResult> Update(
        [FromRoute] string bookId, 
        [FromBody] UpdateBookDTO bookDTO, 
        CancellationToken cancellationToken)
    {
        var book = BookAdapter.ToDomainEntity(bookDTO);
        await _bookService.UpdateAsync(bookId, book, cancellationToken);
        return NoContent();
    }

    [HttpPatch]
    [Route("{bookId}/quantity")]
    public async Task<IActionResult> UpdateBookQuantity(
        [FromRoute] string bookId,
        [FromBody] UpdateBookQuantityDTO bookDTO,
        CancellationToken cancellationToken)
    {
        await _bookService.UpdateQuantityAsync(bookId, (int)bookDTO.Quantity!, cancellationToken);
        return NoContent();
    }

    [HttpPut]
    [Route("{bookId}")]
    public async Task<IActionResult> Replace(
        [FromRoute] string bookId,
        [FromBody] ReplaceBookDTO bookDTO,
        CancellationToken cancellationToken)
    {
        var book = BookAdapter.ToDomainEntity(bookDTO);
        await _bookService.UpdateAsync(bookId, book, cancellationToken);
        return NoContent();
    }

    [HttpDelete]
    [Route("{bookId}")]
    public async Task<IActionResult> Delete(
        [FromRoute] string bookId,
        CancellationToken cancellationToken)
    {
        await _bookService.DeleteAsync(bookId, cancellationToken);
        return NoContent();
    }
}
