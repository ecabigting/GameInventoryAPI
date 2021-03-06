
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameInventoryAPI.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GameInventoryAPI.Repositories 
{
    public class MongoDBItemsRepo : IItemsRepository
    {
        private const string dbName = "gameinventorydB";
        private const string collectionName = "items";
        private readonly IMongoCollection<Item> itemsCollection;
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;
        public MongoDBItemsRepo(IMongoClient mongoClient) 
        {
            IMongoDatabase database = mongoClient.GetDatabase(dbName);
            itemsCollection = database.GetCollection<Item>(collectionName);
        }
        public async Task CreateItemAsync(Item item)
        {
            await itemsCollection.InsertOneAsync(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            // MongoDB needs to use filter builder as it returns as it filters the items
            var filter = filterBuilder.Eq(i => i.Id, id);
            await itemsCollection.DeleteOneAsync(filter);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            // MongoDB needs to use filter builder as it returns as it filters the items
            var filter = filterBuilder.Eq(i => i.Id, id);
            return await itemsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await itemsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            // MongoDB needs to use filter builder as it returns as it filters the items
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
            await itemsCollection.ReplaceOneAsync(filter,item);
        }
    }
}