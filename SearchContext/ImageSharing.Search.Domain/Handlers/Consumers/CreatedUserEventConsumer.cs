using ImageSharing.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace ImageSharing.Search.Domain.Handlers.Consumers;

public class CreatedUserEventConsumer : IConsumer<CreatedUserEvent>
{
    private readonly ILogger<CreatedUserEventConsumer> logger;

    public CreatedUserEventConsumer(ILogger<CreatedUserEventConsumer> logger)
    {
        this.logger = logger;
    }
    public Task Consume(ConsumeContext<CreatedUserEvent> context)
    {
        logger.LogInformation("User created: {UserName}", context.Message.UserName);
        return Task.CompletedTask;
    }
}
