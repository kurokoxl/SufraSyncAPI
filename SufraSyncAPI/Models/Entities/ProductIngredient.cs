namespace SufraSyncAPI.Models.Entities
{
    public class ProductIngredient
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        // How much of the ingredient is used in the product
        public decimal QuantityRequired { get; set; }

        // Unit for the quantity (grams, pieces, ml). Default to pieces
        public string Unit { get; set; } = "pcs";
    }
}