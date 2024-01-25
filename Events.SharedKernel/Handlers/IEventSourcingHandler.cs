using Events.SharedKernel.Domain;

namespace Events.SharedKernel.Handlers;

public interface IEventSourcingHandler<T>
{
    Task SaveAsync(AggregateRoot aggregate);
    Task<T> GetByIdAsync(Guid aggregateId);
}