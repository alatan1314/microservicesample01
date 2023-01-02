using MongoDB.Driver;
using Play.Catalog.Service.Entities;

// add mongo db on docker commad:
//      on ps or command prompt:  docker run -d --rm --name Mongo -p 27017:27017 -v mongodbdata:/data/db mongo

namespace Play.Catalog.Service.Repository
{
    public class ItemsRepository : IItemsRepository
    {
        private const string collectionName = "items";

        private readonly IMongoCollection<Item> dbCollection;

        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

        public ItemsRepository(IMongoDatabase database)
        {
            dbCollection = database.GetCollection<Item>(collectionName);
        }
         
        //public ItemsRepository()
        //{
        //    var mongoClient = new MongoClient("mongodb://localhost:27017");
        //    var database = mongoClient.GetDatabase("Catalog");
        //    dbCollection = database.GetCollection<Item>(collectionName);
        //}


        public async Task<IReadOnlyCollection<Item>> GetAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<Item> GetAsync(Guid id)
        {
            FilterDefinition<Item> filter = filterBuilder.Eq(enti => enti.id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Item item)
        {
            if (item == null)
            {
                throw new ArgumentException(nameof(item));
            }

            await dbCollection.InsertOneAsync(item);
        }

        public async Task UpdateAsync(Item item)
        {
            if (item == null)
            {
                throw new ArgumentException(nameof(item));
            }

            FilterDefinition<Item> filter = filterBuilder.Eq(existingEntity => existingEntity.id, item.id);
            await dbCollection.ReplaceOneAsync(filter, item);
        }

        public async Task RemoveAsync(Item item)
        {
            FilterDefinition<Item> filter = filterBuilder.Eq(existingEntity => existingEntity.id, item.id);

            await dbCollection.DeleteOneAsync(filter);
        }


    }
}
