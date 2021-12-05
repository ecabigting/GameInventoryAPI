
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using GameInventoryAPI.Repositories;
using GameInventoryAPI.Entities;

namespace GameInventoryAPI.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase 
    {
        private readonly InMemItemsRepository repo;
        
        public ItemsController()
        {
            repo = new InMemItemsRepository();
        }

        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            var items = repo.GetItems();
            return items; 
        }
    }
}