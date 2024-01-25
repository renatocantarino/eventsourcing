using Events.SharedKernel.Command;

namespace Posts.Cmd.Api.Commands;

public class AddComentCommand : BaseCommand
{
    public string Comment { get; set; }
    public string UserName { get; set; }
}