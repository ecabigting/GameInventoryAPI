
using System;
using System.Collections.Generic;
using GameInventoryAPI.Entities;
using MongoDB.Driver;

namespace GameInventoryAPI.Repositories 
{
    public class MongoDBItemsRepo : IItemsRepository
    {
        private const string dbName = "gameinventorydB";
        private const string collectionName = "items";
        private readonly IMongoCollection<Item> itemsCollection;
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
            throw new NotImplementedException();
        }

        public Item GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetItems()
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(Item item)
        {
            throw new NotImplementedException();
        }
    }
}