
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using GameInventoryAPI.Repositories;
using GameInventoryAPI.Entities;
using System;
using System.Linq;
using GameInventoryAPI.Dtos;

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
        public ActionResult<ItemDto> GetItems()
        {
            var items = repo.GetItems().Select(i => i.AsDto());

            if(items is null)
            {
                return NotFound();
            }
            return Ok(items);
        }

        [HttpGet("{Id}")]
        public ActionResult<ItemDto> GetItem(Guid Id)
        {
            var item = repo.GetItem(Id);
            if(item is null)
            {
                return NotFound();
            }
            return Ok(item.AsDto());
        }

        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
            Item item = new(){
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
            repo.CreateItem(item);

            // created at action get the action of GetItem, return anon object with new item id, return the dto
            return CreatedAtAction(nameof(GetItem),new {id = item.Id}, item.AsDto());
        }

        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = repo.GetItem(id);
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

            repo.UpdateItem(updatedItem);

            return NoContent();

        }

        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var existingItem = repo.GetItem(id);
            if(existingItem is null)
            {
                return NotFound();
            }

            repo.DeleteItem(id);
            return NoContent();
        }
    }
}