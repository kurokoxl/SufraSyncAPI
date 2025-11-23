using System.ComponentModel.DataAnnotations;

public class CreateProductIngredientDto
{
    [Required]
    public int? IngredientId { get; set; }

    [Range(0.01, 1000, ErrorMessage = "Quantity must be positive")] // Critical check
    public decimal QuantityRequired { get; set; }

}