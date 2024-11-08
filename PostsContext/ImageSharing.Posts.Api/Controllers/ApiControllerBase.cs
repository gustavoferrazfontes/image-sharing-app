using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace ImageSharing.Posts.Api.Controllers;

public class ApiControllerBase: ControllerBase
{
      [NonAction]
      protected IActionResult ResponseResult<T>(Result<T> result)
      {
          var response = new Response<object?>(result.IsSuccess ? result.Value:null, result.IsFailure ? result.Error : string.Empty, result.IsSuccess);
         return result.IsSuccess ? Ok(response) : BadRequest(response);
      }
      protected IActionResult ResponseResult(Result result) 
      {
          var response = new Response<object?>(null,result.IsFailure? result.Error:string.Empty, result.IsSuccess);
          return result.IsSuccess ? Ok(response) : BadRequest(response);
      }
}

public record Response<T>(T Data, string Message, bool Success);