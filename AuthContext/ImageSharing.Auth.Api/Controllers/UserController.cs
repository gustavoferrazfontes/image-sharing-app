using ImageSharing.Auth.Api.Models.Auth.Requests;
using ImageSharing.Auth.Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ImageSharing.Auth.Api.Controllers;

[Route("api/users")]
public class UserController(IMediator mediator) : ApiControllerBase
{
   private readonly IMediator _mediator = mediator;

   [HttpPost("{id}/set-avatar")]
   public async Task<IActionResult> SetAvatar([FromRoute] Guid id,[FromBody] SetAvatarRequest payload)
   {
      var command = new SetUserAvatarCommand(id,payload.Avatar64Base,payload.ImageExtension);
      var result = await _mediator.Send(command);
      return ResponseResult(result);
   }
}