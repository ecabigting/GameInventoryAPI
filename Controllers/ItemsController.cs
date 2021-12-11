
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
    }
}