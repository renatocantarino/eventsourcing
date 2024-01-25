using Events.SharedKernel.Events;

namespace Events.SharedKernel.Infra;

public interface IEventStore
{
    Task SaveAsync(Guid aggregationId, IEnumerable<BaseEvent> events, int expectedVersion);
    Task<IReadOnlyCollection<BaseEvent>> GetAsync(Guid aggregationId);

}