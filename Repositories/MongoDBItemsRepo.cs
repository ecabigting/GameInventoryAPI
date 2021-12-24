
using System;
using System.Collections.Generic;
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
        public void CreateItem(Item item)
        {
            itemsCollection.InsertOne(item);
        }

        public void DeleteItem(Guid id)
        {
            // MongoDB needs to use filter builder as it returns as it filters the items
            var filter = filterBuilder.Eq(i => i.Id, id);
            itemsCollection.DeleteOne(filter);
        }

        public Item GetItem(Guid id)
        {
            // MongoDB needs to use filter builder as it returns as it filters the items
            var filter = filterBuilder.Eq(i => i.Id, id);
            return itemsCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Item> GetItems()
        {
            return itemsCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateItem(Item item)
        {
            // MongoDB needs to use filter builder as it returns as it filters the items
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
            itemsCollection.ReplaceOne(filter,item);
        }
    }
}