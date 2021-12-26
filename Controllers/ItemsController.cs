
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using GameInventoryAPI.Repositories;
using GameInventoryAPI.Entities;
using System;
using System.Linq;
using GameInventoryAPI.Dtos;
using System.Threading.Tasks;

namespace GameInventoryAPI.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase 
    {
        private readonly IItemsRepository repo;
        
        public ItemsController(IItemsRepository itemsRepo)
        {
            repo = itemsRepo;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = (await repo.GetItemsAsync())
                        .Select(i => i.AsDto());
            return items;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid Id)
        {
            var item = await repo.GetItemAsync(Id);
            if(item is null)
            {
                return NotFound();
            }
            return Ok(item.AsDto());
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
        {
            Item item = new(){
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await repo.CreateItemAsync(item);

            // created at action get the action of GetItem, return anon object with new item id, return the dto
            return CreatedAtAction(nameof(GetItemAsync),new {id = item.Id}, item.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = await repo.GetItemAsync(id);
            if(existingItem is null)
            {
                return NotFound();
            }

            // using the 'with' expression from the record type
            // it means we are taking a copy of the existing item
            // and setting the new value for name and price
            // this is required since record is an immutable type
            Item updatedItem = existingItem with {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            await repo.UpdateItemAsync(updatedItem);

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var existingItem = await repo.GetItemAsync(id);
            if(existingItem is null)
            {
                return NotFound();
            }
            await repo.DeleteItemAsync(id);
            return NoContent();
        }
    }
}