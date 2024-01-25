using Events.SharedKernel.Command;
using Events.SharedKernel.Infra;

namespace Posts.Cmd.Infra.Dispatchers;

public class CommandDispatcher : ICommandDispatch
{
   private readonly Dictionary<Type, Func<BaseCommand, Task>> _handlers = new();   
    
    public void RegisterHandler<T>(Func<T, Task> handler) where T : BaseCommand
    {
        if (_handlers.ContainsKey(typeof(T)))
            throw new IndexOutOfRangeException("Handlers already registered");
        
        _handlers.Add(typeof(T) , item => handler((T)item));
    }

    public async Task SendAsync(BaseCommand command)
    {
        if (!_handlers.TryGetValue(command.GetType(), out Func<BaseCommand, Task> handler))
            throw new ArgumentNullException(nameof(handler) , "no command handler register");
        
        
        await handler(command);
    }
}