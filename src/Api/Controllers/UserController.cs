using Api.Adapters;
using Api.DTOs.Request;
using Api.DTOs.Response;
using Api.Extensions;
using Core.Entities.Enums;
using Core.Enums;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRegularUser(
        [FromBody] CreateUserDTO userDTO,
        CancellationToken cancellationToken)
    {
        var user = UserAdapter.ToDomainEntity(userDTO);
        await _userService.CreateRegularUser(user, cancellationToken);

        return NoContent();
    }

    [HttpPost]
    [Route("auth")]
    public async Task<IActionResult> Authenticate(
        [FromBody] AuthenticateUserDTO userDTO,
        CancellationToken cancellationToken)
    {
        var user = UserAdapter.ToDomainEntity(userDTO);
        var token = await _userService.Authenticate(user, cancellationToken);
        if (string.IsNullOrWhiteSpace(token))
        {
            return Unauthorized();
        }

        var response = new SuccessfulAuthenticationDTO(token);

        return Ok(response);
    }

    [Authorize]
    [HttpPatch]
    [Route("password")]
    public async Task<IActionResult> UpdatePassword(
        [FromBody] UpdatePasswordDTO userDTO,
        CancellationToken cancellationToken)
    {
        var usernameClaim = User.GetClaim(JwtClaim.Username);
        await _userService.UpdateUserPassword(usernameClaim?.Value, userDTO.Password, cancellationToken);

        return NoContent();
    }

    [Authorize]
    [HttpPatch]
    [Route("{username}/access-level")]
    public async Task<IActionResult> UpdateAccessLevel(
        [FromRoute] string username,
        [FromBody] UpdateUserAccessLevelDTO userDTO,
        CancellationToken cancellationToken)
    {
        if (!User.HasOwnerAccess())
        {
            return Forbid();
        }

        await _userService.UpdateUserAccessLevel(username, userDTO.AccessLevel, cancellationToken);

        return NoContent();
    }
}
