namespace SufraSyncAPI.Models.Entities
{
    public class Ingredient
    {
        public int IngredientId { get; set; }

        public string Name { get; set; }

        public decimal Stock { get; set; }

        public string Unit { get; set; }

        public List<ProductIngredient> ProductIngredients { get; set; }
    }
}