
using System;
using System.Collections.Generic;
using GameInventoryAPI.Entities;

namespace GameInventoryAPI.Repositories
{
    public interface IItemsRepository
    {
        public Item GetItem(Guid id);
        public IEnumerable<Item> GetItems();
    }

}