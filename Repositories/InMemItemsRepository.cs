using System;
using System.Collections.Generic;
using System.Linq;
using GameInventoryAPI.Entities;

namespace GameInventoryAPI.Repositories 
{
    public class InMemItemsRepository 
    {
        private readonly List<Item> items = new()
        {
            new Item { Id=Guid.NewGuid(), Name="Potion", Price=9, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id=Guid.NewGuid(), Name="Long Sword", Price=222,CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id=Guid.NewGuid(), Name="Buckler", Price=143,CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id=Guid.NewGuid(), Name="Iron Helmet", Price=78,CreatedDate = DateTimeOffset.UtcNow }
        };

        public IEnumerable<Item> GetItems()
        {
            return items;
        }

        public Item GetItem(Guid id)
        {
            return items.Where(i => i.Id == id).SingleOrDefault();
        }
    }
}