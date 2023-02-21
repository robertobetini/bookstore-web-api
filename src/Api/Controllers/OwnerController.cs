using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OwnerController : ControllerBase
{
    private readonly IUserService _userService;

    public OwnerController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOwnerUser(CancellationToken cancellationToken)
    {
        await _userService.CreateOwnerUser(cancellationToken);

        return NoContent();
    }
}
