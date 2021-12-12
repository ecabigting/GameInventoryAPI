
using System.ComponentModel.DataAnnotations;

namespace GameInventoryAPI.Dtos 
{
    public record UpdateItemDto
    {
        [Required]
        public string Name { get; init; }
        [Required]
        [Range(1,999999999)]
        public decimal Price { get; init; }
    }
}