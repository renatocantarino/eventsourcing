using Events.SharedKernel.Command;

namespace Posts.Cmd.Api.Commands;

public class DeletePostCommand : BaseCommand
{
    public Guid PostId { get; set; }
    
    public string UserName { get; set; }
    
}