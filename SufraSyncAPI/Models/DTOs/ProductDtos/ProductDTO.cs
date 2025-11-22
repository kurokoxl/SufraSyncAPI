using Microsoft.EntityFrameworkCore;
using SufraSyncAPI.Models.Entities;

namespace SufraSyncAPI.Models.DTOs.ProductDto
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public List<ProductIngredientDto>? ProductIngredients { get; set; }
    }
}
