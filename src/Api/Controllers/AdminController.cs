using Api.Adapters;
using Api.DTOs.Request;
using Api.Extensions;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    private readonly IUserService _userService;

    public AdminController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreatAdminUser(
        [FromBody] CreateUserDTO userDTO,
        CancellationToken cancellationToken)
    {
        if (!User.HasOwnerAccess())
        {
            return Forbid();
        }

        var user = UserAdapter.ToDomainEntity(userDTO);
        await _userService.CreateAdminUser(user, cancellationToken);

        return NoContent();
    }
}
