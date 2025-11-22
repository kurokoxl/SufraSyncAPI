using SufraSyncAPI.Models.Entities;

namespace SufraSyncAPI.Models.DTOs.CategoryDtos
{
    public class CategoryDto
    {
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}