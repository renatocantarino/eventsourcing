using Events.SharedKernel.Events;

namespace Events.SharedKernel.Domain;

public interface IEventStoreRepository
{
    Task SaveAsync(EventModel @eventModel);

    Task<IReadOnlyList<EventModel>> FindByAggretateId(Guid aggId);
}