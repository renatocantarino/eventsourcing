using Events.SharedKernel.Command;

namespace Posts.Cmd.Api.Commands;

public class EditPostCommand : BaseCommand
{
    public string Message { get; set; }
}