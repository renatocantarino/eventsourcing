using Events.SharedKernel.Handlers;
using Posts.Cmd.Api.Commands.BaseCommandHandlers;
using Posts.Cmd.Domain.Agregattes;

namespace Posts.Cmd.Api.Commands.Handlers;

public class CommandHandler : ICommandHandler
{
    private readonly IEventSourcingHandler<PostAggregate> _eventSourcingHandler;

    public CommandHandler(IEventSourcingHandler<PostAggregate> eventSourcingHandler)
    {
        _eventSourcingHandler = eventSourcingHandler ?? throw new ArgumentNullException(nameof(eventSourcingHandler));
    }

    public async Task HandlerAsync(NewPostCommand command)
    {
        var agg = new PostAggregate(command.Id, command.Author, command.Message);
        await _eventSourcingHandler.SaveAsync(agg);

    }
}