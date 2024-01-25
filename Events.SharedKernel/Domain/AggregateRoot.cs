using Events.SharedKernel.Events;

namespace Events.SharedKernel.Domain;

public abstract class AggregateRoot
{
    protected Guid _id;
    public int Version { get; set; } = -1;
    private readonly List<BaseEvent> _changes = new();

    public Guid Id => _id;

    
    
    public IEnumerable<BaseEvent> GetUncommitedChanges() => _changes;
    public void MarkAllEventsCommited() => _changes.Clear();
    private void ApplyChanges(BaseEvent @event, bool isNew)
    {
        var method = this.GetType().GetMethod("Apply", [@event.GetType()]);

        if (method == null) 
            throw new ArgumentNullException(nameof(method), "Apply not found");

        method.Invoke(this, [@event]);
        
        if(isNew)
            _changes.Add(@event);


    }

    protected void RaiseEvent(BaseEvent @event)
    {
        ApplyChanges(@event, true);
    }

    public void ReplayEvent(IEnumerable<BaseEvent> events)
    {
        foreach (var @evt in events)
        {
            ApplyChanges(@evt, false);
        }
    }
}