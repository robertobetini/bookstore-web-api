using Api.Extensions;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("books")]
public class BookPhotosController : ControllerBase
{
    private readonly IBookPhotoService _bookPhotoService;

    public BookPhotosController(IBookPhotoService bookPhotoService)
    {
        _bookPhotoService = bookPhotoService;
    }

    [Authorize]
    [HttpPost]
    [Route("{bookId}/photos")]
    public async Task<IActionResult> UploadPhoto(
        [FromRoute] string bookId,
        IFormFile photo,
        CancellationToken cancellationToken)
    {
        if (!User.HasAdminAccess())
        {
            return Forbid();
        }

        await _bookPhotoService.UploadPhotoAsync(
            bookId,
            photo.OpenReadStream(),
            photo.FileName,
            photo.ContentType,
            cancellationToken);

        return Ok();
    }

    [Authorize]
    [HttpDelete]
    [Route("{bookId}/photos/{photoId}")]
    public async Task<IActionResult> RemovePhoto(
        [FromRoute] string bookId,
        [FromRoute] string photoId,
        CancellationToken cancellationToken)
    {
        if (!User.HasAdminAccess())
        {
            return Forbid();
        }

        await _bookPhotoService.RemovePhotoAsync(
            bookId,
            photoId,
            cancellationToken);

        return Ok();
    }
}
