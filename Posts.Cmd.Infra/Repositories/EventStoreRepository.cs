using Events.SharedKernel.Domain;
using Events.SharedKernel.Events;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Posts.Cmd.Infra.Configs;

namespace Posts.Cmd.Infra.Repositories;

public class EventStoreRepository : IEventStoreRepository
{

    private readonly IMongoCollection<EventModel> _eventStoreCollection;

    public EventStoreRepository(IOptions<MongoConfig> mongoConfig)
    {
        var mongoClient = new MongoClient(mongoConfig.Value.ConnectionString);
        var _database = mongoClient.GetDatabase(mongoConfig.Value.Database);
        this._eventStoreCollection = _database.GetCollection<EventModel>(mongoConfig.Value.Collection);
    }
    
    public async Task SaveAsync(EventModel @eventModel)
    {
        await _eventStoreCollection.InsertOneAsync(@eventModel).ConfigureAwait(false);
    }

    public async Task<IReadOnlyList<EventModel>> FindByAggretateId(Guid aggId)
    {
        return await _eventStoreCollection
                        .Find(x => x.AggregateIdentifier == aggId)
                        .ToListAsync().ConfigureAwait(false);
    }
}