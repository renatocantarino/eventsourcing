using Events.SharedKernel.Infra;
using Microsoft.AspNetCore.Mvc;
using Posts.Cmd.Api.Commands;
using Posts.Cmd.Api.DTOs;

namespace Posts.Cmd.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class NewPostController : ControllerBase
{
    private readonly ILogger<NewPostController> _logger;
    private readonly ICommandDispatch _commandDispatcher;
    
    public NewPostController(ILogger<NewPostController> logger, ICommandDispatch commandDispatcher)
    {
        _logger = logger;
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost]
    public async Task<ActionResult> NewPostAsync(NewPostCommand command)
    {
        var id = Guid.NewGuid();
        try
        {
            command.Id = id;
            await _commandDispatcher.SendAsync(command);

            return StatusCode(StatusCodes.Status201Created,
                new NewPostResponse()
                {
                    Id = command.Id,
                    Message = "Post Created"

                });

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
}