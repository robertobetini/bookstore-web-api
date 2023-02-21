using Api.Adapters;
using Api.DTOs.Request;
using Api.DTOs.Response;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public UserController(
        IUserService userService,
        ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateUserDTO userDTO,
        CancellationToken cancellationToken)
    {
        var user = UserAdapter.ToDomainEntity(userDTO);
        await _userService.Create(user);

        return NoContent();
    }

    [HttpPost]
    [Route("auth")]
    public async Task<IActionResult> Authenticate(
        [FromBody] AuthenticateUserDTO userDTO,
        CancellationToken cancellationToken)
    {
        var user = UserAdapter.ToDomainEntity(userDTO);
        var token = await _userService.Authenticate(user);
        if (string.IsNullOrWhiteSpace(token))
        {
            return Unauthorized();
        }

        var response = new SuccessfulAuthenticationDTO(token);

        return Ok(response);
    }
}
