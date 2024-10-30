// Ignore Spelling: Auth

using ImageSharing.Auth.Api.Models.Auth.Requests;
using ImageSharing.Auth.Domain.Commands;
using ImageSharing.Auth.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ImageSharing.Auth.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(TokenGenerator tokenGenerator, IMediator mediator)
    : ControllerBase
{

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest payload)
    {
        var result = await mediator.Send(new LoginCommand(payload.Email,payload.Password));

        var generatedToken = tokenGenerator.GenerateToken([new Claim(ClaimTypes.Name, "John Doe")]);

        return Ok(new { access_token = generatedToken });
    }

    [HttpPost("signup")]
    public async Task<IActionResult> CreateUser([FromBody] SignInRequest payload)
    {
        await mediator.Send(new CreateNewUserCommand(payload.Name, payload.Email, payload.Password, payload.ConfirmPassword));
        return Ok();
    }

}
