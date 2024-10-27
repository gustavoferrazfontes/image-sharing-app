// Ignore Spelling: Auth

using ImageSharing.Auth.Domain.Commands;
using ImageSharing.Auth.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Security.Claims;

namespace ImageSharing.Auth.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly TokenGenerator tokenGenerator;
    private readonly IMediator mediator;
    private readonly IConnection _connection;

    public AuthController(TokenGenerator tokenGenerator,IMediator mediator)
    {
        this.tokenGenerator = tokenGenerator;
        this.mediator = mediator;
        //var factory = new ConnectionFactory()
        //{
        //    HostName = "172.17.0.1",
        //};

        //_connection = factory.CreateConnection();
    }
    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login()
    {
        var generatedToken = tokenGenerator.GenerateToken(new List<Claim>
        {
            new Claim(ClaimTypes.Name, "John Doe")
        });

        //using var channel = _connection.CreateModel();
        //channel.QueueDeclare(queue: "q.created-users.events",
        //                     durable: false,
        //                     exclusive: false,
        //                     autoDelete: false,
        //                     arguments: null);

        //var authPayload = JsonSerializer.Serialize(new { email = "john.doe@gmail.com", name = "John Doe" });
        //var body = Encoding.UTF8.GetBytes(authPayload);

        //channel.BasicPublish(exchange: "",
        //                     routingKey: "q.created-users.events",
        //                     basicProperties: null,
        //                     body: body);

        return Ok(new { access_token = generatedToken });
    }

    [HttpPost("signup")]
    public async Task<IActionResult> CreateUser()
    {

        await this.mediator.Send(new CreateNewUserCommand("John Doe", "johndoe@gmail.com", "123456", ""));
        return Ok();
    }

}
