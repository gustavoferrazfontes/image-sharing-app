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
    : ApiControllerBase 
{

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest payload)
    {
        var result = await mediator.Send(new LoginCommand(payload.Email,payload.Password));

        if (result.IsFailure) return ResponseResult(result);
        var generatedToken = tokenGenerator.GenerateToken([new Claim(ClaimTypes.Name, "John Doe")]);
        
        return Ok(new Response<object?>(generatedToken, null, true));
    }

    [HttpPost("signup")]
    public async Task<IActionResult> CreateUser([FromBody] SignInRequest payload)
    {
        var command = new CreateNewUserCommand(payload.Name, payload.Email, payload.Password, payload.ConfirmPassword);
        var result = await mediator.Send(command);
        return ResponseResult(result);
    }

}
