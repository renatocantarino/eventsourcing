namespace Events.SharedKernel.Messages;

public class Message
{
    public Guid Id { get; set; }
    public DateTime AddedAt { get; set; }


    protected Message()
    {
        AddedAt = DateTime.Now;
    }
}