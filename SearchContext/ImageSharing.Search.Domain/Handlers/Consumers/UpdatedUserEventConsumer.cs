using ImageSharing.Contracts;
using ImageSharing.Search.Domain.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace ImageSharing.Search.Domain.Handlers.Consumers;

public class UpdatedUserEventConsumer(ILogger<UpdatedUserEventConsumer> logger,ISearchRepository searchRepository):IConsumer<UpdatedUserEvent>
{
    public async Task Consume(ConsumeContext<UpdatedUserEvent> context)
    {
           var result =  await searchRepository.UpdateAsync(context.Message);    
           if (result.IsFailure)
               logger.LogError(result.Error);
           else
               logger.LogInformation("User added to search index");
    }
}