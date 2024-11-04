using ImageSharing.Contracts;
using ImageSharing.Search.Domain.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace ImageSharing.Search.Domain.Handlers.Consumers;

public class CreatedUserEventConsumer(ILogger<CreatedUserEventConsumer> logger, ISearchRepository searchRepository)
    : IConsumer<UserCreatedEvent>
{
    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        var result = await searchRepository.AddAsync(context.Message);

        if (result.IsFailure)
            logger.LogError(result.Error);
        else
            logger.LogInformation("User added to search index");
    }
}
