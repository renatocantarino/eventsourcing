using Events.SharedKernel.Domain;
using Events.SharedKernel.Handlers;
using Events.SharedKernel.Infra;
using Posts.Cmd.Api.Commands;
using Posts.Cmd.Api.Commands.BaseCommandHandlers;
using Posts.Cmd.Api.Commands.Handlers;
using Posts.Cmd.Domain.Agregattes;
using Posts.Cmd.Infra.Configs;
using Posts.Cmd.Infra.Dispatchers;
using Posts.Cmd.Infra.Handlers;
using Posts.Cmd.Infra.Repositories;
using Posts.Cmd.Infra.Stores;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .Configure<MongoConfig>(builder.Configuration.GetSection(nameof(MongoConfig)));

builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddScoped<IEventSourcingHandler<PostAggregate>, EventSourcingHandler>();
builder.Services.AddScoped<ICommandHandler, CommandHandler>();


var commandHandler = builder.Services.BuildServiceProvider().GetRequiredService<ICommandHandler>();
var dispatcher = new CommandDispatcher();
dispatcher.RegisterHandler<NewPostCommand>(commandHandler.HandlerAsync);
builder.Services.AddSingleton<ICommandDispatch>(_ => dispatcher);



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

