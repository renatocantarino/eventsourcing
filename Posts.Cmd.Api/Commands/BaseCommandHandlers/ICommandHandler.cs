namespace Posts.Cmd.Api.Commands.BaseCommandHandlers;

public interface ICommandHandler
{
    Task HandlerAsync(NewPostCommand command);
    
    //all commands registered here
}