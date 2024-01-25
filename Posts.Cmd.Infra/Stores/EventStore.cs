using Events.SharedKernel.Domain;
using Events.SharedKernel.Events;
using Events.SharedKernel.Infra;
using Events.SharedKernel.Exceptions;
using Posts.Cmd.Domain.Agregattes;

namespace Posts.Cmd.Infra.Stores;

public class EventStore : IEventStore
{
    private readonly IEventStoreRepository _eventStoreRepository;
    public EventStore(IEventStoreRepository eventStoreRepository)
    {
        _eventStoreRepository = eventStoreRepository ?? throw new ArgumentNullException(nameof(eventStoreRepository));
    }


    public async Task<IReadOnlyCollection<BaseEvent>> GetAsync(Guid aggregationId)
    {
        var eventStream = await _eventStoreRepository.FindByAggretateId(aggregationId);

       var ordered =  eventStream.OrderBy(x => x.Version)
                                                .Select(it => it.EventData).ToList().AsReadOnly();

        return ordered;
    }
        

    public async Task SaveAsync(Guid aggregationId, IEnumerable<BaseEvent> events, int expectedVersion)
    {
        var eventStream = await _eventStoreRepository.FindByAggretateId(aggregationId);
        if (expectedVersion != -1 && eventStream[^1].Version != expectedVersion)
            throw new ConcurrencyException();
        
        
        var version = expectedVersion;

        foreach (var @event in events)
        {
            version++;
            @event.Version =  version++;
            var eventType = @event.GetType().Name;
            var eventModel = new EventModel
            {
                TimeStamp = DateTime.Now,
                AggregateIdentifier = aggregationId,
                AggregateType = nameof(PostAggregate),
                Version = version,
                EventType = eventType,
                EventData = @event
            };
            
            await _eventStoreRepository.SaveAsync(eventModel);
        }
    }
}