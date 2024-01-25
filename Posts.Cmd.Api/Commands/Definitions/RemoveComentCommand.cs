using Events.SharedKernel.Command;

namespace Posts.Cmd.Api.Commands;

public class RemoveComentCommand : BaseCommand
{
    public Guid CommentId { get; set; }
    public string UserName { get; set; }
}