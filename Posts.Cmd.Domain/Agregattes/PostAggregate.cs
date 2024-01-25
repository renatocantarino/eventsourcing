using Events.SharedKernel.Domain;
using Post.Common.Events;

namespace Posts.Cmd.Domain.Agregattes;

public class PostAggregate : AggregateRoot
{
    private bool _active;
    private string _author;
    private readonly Dictionary<Guid, Tuple<string, string>> _comments = new();


    public bool Active
    {
        get => _active;
        set => _active = value;
    }

    public PostAggregate() { }

    public PostAggregate(Guid id, string author, string message)
    {
        RaiseEvent(new PostAddedEvent
        {
            Id = id,
            Message = message,
            AddedAt = DateTime.Now,
            Author = author
        });
    }

    public void Apply(PostAddedEvent @event)
    {
        _id = @event.Id;
        _active = true;
        _author = @event.Author;
    }

    public void EditMessage(string msg)
    {
        if (!_active)
            throw new InvalidOperationException("Post inactive!");

        if (string.IsNullOrEmpty(msg))
            throw new InvalidOperationException("Message cannot be null or empty");
        
       //raise de updateComment
        
    }
}