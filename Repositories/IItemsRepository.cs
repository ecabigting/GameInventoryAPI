
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameInventoryAPI.Entities;

namespace GameInventoryAPI.Repositories
{
    public interface IItemsRepository
    {
        Task<Item> GetItemAsync(Guid id);
        Task<IEnumerable<Item>> GetItemsAsync();
        Task CreateItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(Guid id);
    }

}