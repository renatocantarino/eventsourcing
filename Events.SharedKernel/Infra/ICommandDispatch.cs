using Events.SharedKernel.Command;

namespace Events.SharedKernel.Infra;

public interface ICommandDispatch
{
    void RegisterHandler<T>(Func<T , Task> handler) where T: BaseCommand;
    Task SendAsync(BaseCommand command);
}