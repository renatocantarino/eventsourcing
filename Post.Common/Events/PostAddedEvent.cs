using Events.SharedKernel.Events;

namespace Post.Common.Events;

public class PostAddedEvent : BaseEvent
{
    public string Author { get; set; }
    public string Message { get; set; }
    
    public PostAddedEvent() : base(nameof(PostAddedEvent))
    {
    }
}