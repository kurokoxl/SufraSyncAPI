namespace SufraSyncAPI.Models.DTOs.ProductDto
{
    public class ProductIngredientDto
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public decimal QuantityRequired { get; set; }
        public string Unit { get; set; }
    }
}
