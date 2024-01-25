using Events.SharedKernel.Domain;
using Events.SharedKernel.Handlers;
using Events.SharedKernel.Infra;
using Posts.Cmd.Domain.Agregattes;

namespace Posts.Cmd.Infra.Handlers;

public class EventSourcingHandler : IEventSourcingHandler<PostAggregate>
{
    private readonly IEventStore _eventStore;

    public EventSourcingHandler(IEventStore eventStore)
    {
        _eventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
    }

    public async Task SaveAsync(AggregateRoot aggregate)
    {
        await _eventStore.SaveAsync(aggregate.Id, aggregate.GetUncommitedChanges(), aggregate.Version);
        aggregate.MarkAllEventsCommited();
    }

    public async Task<PostAggregate> GetByIdAsync(Guid aggregateId)
    {
        var aggregate = new PostAggregate();
        var events = await _eventStore.GetAsync(aggregateId);

        if (events == null || !events.Any())
            return aggregate;
        
        aggregate.ReplayEvent(events);
        aggregate.Version = events.Max(x => x.Version);
        
        return aggregate;
    }
}