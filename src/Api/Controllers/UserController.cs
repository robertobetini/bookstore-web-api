using Api.Adapters;
using Api.DTOs.Request;
using Api.DTOs.Response;
using Core.Entities;
using Core.Services.Interfaces;
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
}
