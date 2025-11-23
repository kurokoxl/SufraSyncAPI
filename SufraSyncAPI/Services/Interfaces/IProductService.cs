using SufraSyncAPI.Models.DTOs.ProductDto;

public interface IProductService
{
    // Reads
    Task<IEnumerable<ProductDTO>> GetAllProductsAsync(int? categoryId = null);
    Task<IEnumerable<ProductDTO>> GetProductsByCategoryOld(int categoryId); 
    Task<ProductDTO?> GetProductAsync(int id);

    // Writes (Product)
    Task<ProductDTO?> AddProductAsync(CreateProductDto createDto);
    Task<ProductDTO?> UpdateProductAsync(int id, UpdateProductDTO updateDto);
    Task<ProductDTO?> DeleteProductAsync(int id); 

    // Writes (Ingredients)
    Task<ProductIngredientDto?> AddIngredientToProductAsync(int productId, CreateProductIngredientDto createDto); // <--- New
    Task<ProductIngredientDto?> UpdateProductIngredientAsync(int id, int ingredientId, UpdateProductIngredientDto updateDto);
    Task<bool> RemoveIngredientFromProductAsync(int productId, int ingredientId);
        
}