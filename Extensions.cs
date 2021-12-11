
using GameInventoryAPI.Dtos;
using GameInventoryAPI.Entities;

namespace GameInventoryAPI 
{
    public static class Extension {
        public static ItemDto AsDto(this Item i)
        {
            return  new ItemDto{
                CreatedDate = i.CreatedDate,
                Id = i.Id,
                Name = i.Name,
                Price = i.Price
            };
        }

    }
}