using Events.SharedKernel.Command;

namespace Posts.Cmd.Api.Commands;

public class EditComentCommand : BaseCommand
{
    public Guid CommentId { get; set; }
    public string Comment { get; set; }
    public string UserName { get; set; }
}