namespace SufraSyncAPI.Models.Entities
{
    public class Ingredient
    {
        public int IngredientId { get; set; }

        public string Name { get; set; }

        public int Stock { get; set; }

        public List<ProductIngredient> ProductIngredients { get; set; }
    }
}