using ImageSharing.Search.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ImageSharing.Search.Api.Controllers;

[Route("api/searches")]
public class SearchController : ApiControllerBase 
{
    private readonly IMediator _mediator;

    public SearchController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("users")]
    public async Task<IActionResult> Index([FromQuery] int pagaSize, string? lastResultId)
    {
        var query = new GetUsersQuery(pagaSize,lastResultId);
        var result = await _mediator.Send(query);

        return ResponseResult(result);

    }
}