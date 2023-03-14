using Api.Adapters;
using Api.DTOs.Request;
using Api.Extensions;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<IActionResult> GetOne(
        [FromRoute] string bookId, 
        CancellationToken cancellationToken)
    {
        var book = await _bookService.GetOneAsync(bookId, cancellationToken: cancellationToken);
        var response = BookAdapter.ToGetBookDTO(book);
        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateBookDTO bookDTO, 
        CancellationToken cancellationToken)
    {
        if (!User.HasAdminAccess())
        {
            return Forbid();
        }

        var book = BookAdapter.ToDomainEntity(bookDTO);
        var createdBookId = await _bookService.CreateAsync(book, cancellationToken);
        return Ok(createdBookId);
    }

    [Authorize]
    [HttpPatch]
    [Route("{bookId}")]
    public async Task<IActionResult> Update(
        [FromRoute] string bookId,
        [FromBody] UpdateBookDTO bookDTO,
        CancellationToken cancellationToken)
    {
        if (!User.HasAdminAccess())
        {
            return Forbid();
        }

        var book = BookAdapter.ToDomainEntity(bookDTO);
        await _bookService.UpdateAsync(bookId, book, cancellationToken);
        return NoContent();
    }

    [Authorize]
    [HttpPut]
    [Route("{bookId}")]
    public async Task<IActionResult> Replace(
        [FromRoute] string bookId,
        [FromBody] ReplaceBookDTO bookDTO,
        CancellationToken cancellationToken)
    {
        if (!User.HasAdminAccess())
        {
            return Forbid();
        }

        var book = BookAdapter.ToDomainEntity(bookDTO);
        await _bookService.UpdateAsync(bookId, book, cancellationToken);
        return NoContent();
    }

    [Authorize]
    [HttpDelete]
    [Route("{bookId}")]
    public async Task<IActionResult> Delete(
        [FromRoute] string bookId,
        CancellationToken cancellationToken)
    {
        if (!User.HasAdminAccess())
        {
            return Forbid();
        }

        await _bookService.DeleteAsync(bookId, cancellationToken);
        return NoContent();
    }
}
